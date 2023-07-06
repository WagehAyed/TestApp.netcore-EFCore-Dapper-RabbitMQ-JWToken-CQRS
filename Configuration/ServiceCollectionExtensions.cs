using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace TestApp.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static  IConfigurationBuilder AddDatabaseConfiguration( this IServiceCollection service,IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder();
            service.AddSingleton(builder);
            var options = configuration.GetSection("Configuration").Get<DatabaseConfigurationOptions>();
            builder.Add(new DatabaseConfigurationSource(options));
            return builder;
        }
    }
}
