

using TestApp.Api.Middlewares;
using TestApp.Application.Queries;
using TestApp.Application.Security.Middlewares;
using TestApp.Configuration;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using TestApp.Application.Commands;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

using System.Collections.Generic;
using MassTransit;
using TestApp.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using TestApp.Api.ModelBinding;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Text.Json;
using Dapper;
using TestApp.Api.Middlewares;
using TestApp.Application.Security.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using Microsoft.Extensions.Caching.Distributed;
using TestApp.Common;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TestApp.Application.Queries;
using Microsoft.AspNetCore.SignalR;
using FluentValidation;
using FluentValidation.AspNetCore;
using TestApp.Api.Filters;
using System.Globalization;
using Microsoft.AspNetCore.Routing;
using StackExchange.Redis;
using TestApp.Application.Services;
using TestApp.Application.Json;
using IdentityModel;
using TestApp.Application.Queries.UserManagement;
using TestApp.Application.Services.UserManagement;

namespace TestApp.Api
{
    public class ModuleInitializer
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _policySchemeName = "Default";
        public IConfiguration Configuration { get; private set; }
        public UpsAuthenticationOptions UpsAuthenticationOptions { get; private set; }
        public ModuleInitializer(IConfiguration configuration,IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;

        }
        public virtual void ConfigureServices(IServiceCollection services)
        {
            BuildConfiguration(services);
            ConfigureOptions(services);
            ConfigureSerilog(services);
            ConfigureQueryExecuter(services);
            services.ScanAndRegisterDependencies();
            services.AddHttpContextAccessor();
            AddControllers(services);
            ConfigureSwagger(services);
            ConfigureAuth(services);
            AddCache(services);
        }
        protected void BuildConfiguration(IServiceCollection services)
        {
            Configuration = services.AddDatabaseConfiguration(Configuration).Build();
            UpsAuthenticationOptions = Configuration.GetSection("UpsAuthentication").Get<UpsAuthenticationOptions>();
            services.AddSingleton(Configuration);
        }
        protected virtual void ConfigurationOptions(IServiceCollection services)
        {
            services.Configure<UpsAuthenticationOptions>(Configuration.GetSection("UpsAuthentication"));
        }
        protected void ConfigureMvc(MvcOptions options)
        {
            options.Filters.Add(new AuthorizeFilter(_policySchemeName));
            options.Filters.Add<AuthorizeActionFilter>();

            var bodyProvider = options.ModelBinderProviders.Single(provider => provider is BodyModelBinderProvider) as BodyModelBinderProvider;
            var complexProvider = options.ModelBinderProviders.Single(provider => provider is ComplexObjectModelBinderProvider) as ComplexObjectModelBinderProvider;
            var bodyAndRouteProvider = new MultipleSourcesModelBinderProvider(bodyProvider, complexProvider);
            options.ModelBinderProviders.Insert(0, bodyAndRouteProvider);
        }
        protected void ConfigureSerilog(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithMachineName()
                .Enrich.WithClientAgent()
                .Enrich.WithClientIp()
                .Enrich.WithExceptionDetails()
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(Configuration["Elasticsearch:Host"]))
                {
                    AutoRegisterTemplate= true,
                    AutoRegisterTemplateVersion= AutoRegisterTemplateVersion.ESv7,
                    IndexFormat = Configuration["Elasticsearch:IndexName"],
                    TypeName=null,
                    BatchAction=ElasticOpType.Create,
                    ModifyConnectionSettings= cfg =>
                    {
                        if (!string.IsNullOrEmpty(Configuration["Elasticsearch:UserName"]) && !string.IsNullOrEmpty(Configuration["Elasticsearch:Password"]))
                            return cfg.BasicAuthentication(Configuration["Elasticsearch:UserName"], Configuration["Elasticsearch:Password"]);
                        return cfg;
                    }
                    ,EmitEventFailure=EmitEventFailureHandling.ThrowException
                })
                .Destructure.ToMaximumDepth(1)
                .CreateLogger();

            services.AddLogging(builder => builder.AddSerilog(dispose: true));
            var loggerFactory=new LoggerFactory().AddSerilog(Log.Logger);
            services.AddSingleton(loggerFactory.CreateLogger(_policySchemeName));
            Serilog.Debugging.SelfLog.Enable(msg => System.Diagnostics.Debug.WriteLine("Serilog " + msg));
        }
        private void ConfigureQueryExecuter(IServiceCollection services)
        {
            var options = new QueryExecuterOptions
            {
                ConnectionString = Configuration.GetConnectionString("Application")
            };
            ConfigureQueryExecuterOptions(options);
            services.AddSingleton(options);
        }
        protected virtual void ConfigureQueryExecuterOptions(QueryExecuterOptions options) { }
        protected void ConfigureJsonOptions(JsonOptions options)
        {
            options.JsonSerializerOptions.Converters.AddRange(JsonSerializerExtensions.DefaultConverters);
            options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;

        }
        protected void AddControllers(IServiceCollection services)
        {
            services.AddControllers(ConfigureMvc)
                .AddFluentValidation()
                .AddJsonOptions(ConfigureJsonOptions);

            ValidatorOptions.Global.LanguageManager.Enabled= true;
            ValidatorOptions.Global.LanguageManager.Culture=new CultureInfo("ar");
        }
        protected void ConfigureSwagger(IServiceCollection services)
        {
            if (!_env.IsDevelopment())
                return;
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
        }
        protected virtual void ConfigureOptions(IServiceCollection services)
        {
            services.Configure<UpsAuthenticationOptions>(Configuration.GetSection("UpsAuthentication"));
        }
        public void Configure(IApplicationBuilder app,IWebHostEnvironment env)
        {
            if (bool.Parse(Configuration["Diagnostics:Enabled"]))
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/error");

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseUpsAuthentication(UpsAuthenticationOptions);
            app.UseMiddleware<CorrelationIdMiddleware>();
            if (_env.IsDevelopment()) { 
            UseSwagger(app);
            app.UseCors();
            }
            else
            {
                app.UseCors("AllowCors");
            }
            app.UseEndpoints(endPoints =>
            {
                endPoints.MapControllers();
                RegisterEndPoints(endPoints);
            });
        }
        protected virtual void UseSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Assembly.GetEntryAssembly().GetName().Name} v1"));
        }
        
        //protected void ConfigureAuth(IServiceCollection services)
        //{
        //    var upsAuthenticationOptions = Configuration.GetSection("UpsAuthentication");
        ////services.Addup
        //}
        protected void ConfigureAuth(IServiceCollection services)
        {
            var upsAuthenticationOptions = Configuration.GetSection("UpsAuthentication").Get<UpsAuthenticationOptions>();

            services.AddUpsAuthentication(_policySchemeName, opt =>
            {
                opt.Authority = UpsAuthenticationOptions.Authority;
                opt.RequireHttpsMetadata = UpsAuthenticationOptions.RequireHttpsMetadata;
                opt.Audience = UpsAuthenticationOptions.Audience;
                opt.ApplicationUUID = UpsAuthenticationOptions.ApplicationUUID;
                opt.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async ctx =>
                    {
                        string identityNumber = ctx.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
                        var cache = ctx.HttpContext.RequestServices.GetRequiredService<IDistributedCache>();
                        var applicationUser = new Dictionary<string, object>();
                        var applicationUserString = await cache.GetStringAsync($"{upsAuthenticationOptions.ApplicationUUID}.{identityNumber}:User");
                        if (string.IsNullOrEmpty(applicationUserString))
                        {
                            var applicationUserInfo = await GetCurrentUserInfo(identityNumber, ctx.HttpContext);
                            if (applicationUserInfo == null)
                            {
                                ctx.Fail("User not found in Inv");
                                return;
                            }
                            applicationUserString = JsonSerializer.Serialize(applicationUserInfo);
                            await cache.SetStringAsync($"{upsAuthenticationOptions.ApplicationUUID}.{identityNumber}:User", applicationUserString);
                        }
                        applicationUser = JsonSerializer.Deserialize<Dictionary<string, object>>(applicationUserString);
                        var claimTypes = GetCustomClaims();
                        var claims = new List<Claim>();
                        claimTypes.ForEach(x => claims.Add(new Claim(x, applicationUser[x]?.ToString() ?? string.Empty)));
                        var appIdentity = new ClaimsIdentity(claims);
                        ctx.Principal.AddIdentity(appIdentity);

                    },
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.Request.Path;

                        if (!string.IsNullOrWhiteSpace(accessToken) && path.StartsWithSegments("/hubs"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }
            protected virtual async Task<object> GetCurrentUserInfo(string identityNumber, HttpContext httpContext)
            {
                var userQueries = httpContext.RequestServices.GetRequiredService<IUserQueries>();
                return await userQueries.FindUserByIdentityNumber<ApplicationUser>(identityNumber);
            }

        protected virtual List<string> GetCustomClaims()
        {
            return new List<string>
                            {
                              Common.JwtClaimTypes.SectorIdClaimType,
                              Common.JwtClaimTypes.DepartmentIdClaimType,
                              Common.JwtClaimTypes.EmployeeIdClaimType,
                              Common.JwtClaimTypes.WorkScopeIdClaimType,
                              Common.JwtClaimTypes.IdClaimType,
                              Common.JwtClaimTypes.UUIDClaimType,
                              Common.JwtClaimTypes.BranchIdClaimType,
                              Common.JwtClaimTypes.DivisionIdClaimType,
                            };
        }

        virtual protected void RegisterEndPoints(IEndpointRouteBuilder endPoints)
        {

        }

        protected void AddCache(IServiceCollection services)
        {
            if(_env.IsDevelopment())
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddStackExchangeRedisCache(opt => opt.Configuration = Configuration["Redis:ConnectionString"]);
            }
        }
    }
}
