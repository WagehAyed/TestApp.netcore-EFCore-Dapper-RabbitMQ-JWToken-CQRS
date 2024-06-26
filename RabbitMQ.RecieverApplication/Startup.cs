using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.RecieverApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RabbitMQ.RecieverApplication", Version = "v1" });
            });
            services.AddMassTransit(x =>
            {
                x.AddConsumer<SenderTutorial>();
                x.AddConsumer<SenderTutorial2>();

                x.AddConsumer<PublisherTutorial>();
                x.AddConsumer<PublisherTutorial2>();
                x.AddConsumer<PublisherTutorial3>();
                x.AddConsumer<RequestResponceTutorial>();
                x.UsingRabbitMq((context, config) =>
                {
                    config.Host(new Uri("rabbitmq://localhost"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    // command Send
                    config.ReceiveEndpoint(queueName:"send-tutorial", configureEndpoint: e =>
                    {
                        e.Consumer<SenderTutorial>();
                        e.Consumer<SenderTutorial2>();

                    });

                    // Publish 
                    config.ReceiveEndpoint("publish-tutorial", e =>
                    {
                        e.Consumer<PublisherTutorial>();
                        e.Consumer<PublisherTutorial2>();
                        e.Consumer<PublisherTutorial3>();

                    });

                    //request Responce Tutorial 
                    config.ReceiveEndpoint("requestAndResponce-tutorial", e =>
                    {
                        e.Consumer<RequestResponceTutorial>();
                    });
                });
                
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RabbitMQ.RecieverApplication v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
