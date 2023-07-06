using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Application.Security.PolicyRequirments;

namespace TestApp.Application.Security.Middlewares
{
    public static class UPSMiddlewareExtensions
    {
        public static IApplicationBuilder UseUpsAuthentication(this IApplicationBuilder app,UpsAuthenticationOptions configureOptions=default)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            return app.UseMiddleware<ApplicationValidationMiddleware>(configureOptions.Authority, configureOptions.ApplicationUUID);

        }
        public static IServiceCollection AddUpsAuthentication(this IServiceCollection services, string authorizationPolicy, Action<UpsAuthenticationOptions>? configureOptions = default)
        {

            var upsAuthenticationOptions = new UpsAuthenticationOptions();
            configureOptions?.Invoke(upsAuthenticationOptions);
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = upsAuthenticationOptions.AuthenticationScheme;
                opt.DefaultChallengeScheme = upsAuthenticationOptions.AuthenticationScheme;
            }).AddJwtBearer(upsAuthenticationOptions.AuthenticationScheme, opt =>
            {
                opt.RequireHttpsMetadata = upsAuthenticationOptions.RequireHttpsMetadata;
                opt.Authority = upsAuthenticationOptions.Authority;
                opt.Audience = upsAuthenticationOptions.Audience;
                opt.TokenValidationParameters.ValidateAudience = true;
                opt.SaveToken = true;
                opt.MapInboundClaims = true;
                opt.Events = upsAuthenticationOptions.Events;
            });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(authorizationPolicy, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.AddAuthenticationSchemes(upsAuthenticationOptions.AuthenticationScheme);
                    policy.Requirements.Add(new MustBeAssignedToApplicationRequirement(upsAuthenticationOptions.ApplicationUUID));
                }
                );
            });
            return services;
        }
    }
}
