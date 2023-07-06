using TestApp.Api.Middlewares;
using TestApp.Api.ModelBinding;
using TestApp.Application.DI;
using TestApp.Application.Queries;
using TestApp.Application.Security.Middlewares;
using TestApp.Common;
using TestApp.Domain;
using MassTransit; 
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using TestApp.Application.Services;
using System.Collections.Generic;
using TestApp.Infrastructure.Persistence;
namespace TestApp.Api
{
    public static class DependencyScanner
    {
        public const string RootNameSpace = "TestApp.";
        private static IEnumerable<Assembly> _projectAssemblies = null;
        private static IEnumerable<Assembly> GetProjectAssemblies()
        {
            if (_projectAssemblies == null) {
                _projectAssemblies=AppDomain.CurrentDomain.GetAssemblies().Where(x => !x.IsDynamic && x.FullName.StartsWith(RootNameSpace)).ToList();
            }
            return _projectAssemblies;

        }


        

        private static bool IsExcludedType(Type type)
{
            return type.IsAssignableTo(typeof(Exception)) ||
                type.IsAssignableTo(typeof(IConsumerDefinition)) ||
                type.IsAssignableTo(typeof(IdentifiedObject)) ||
                type.IsAssignableTo(typeof(ICommand)) ||
                type.IsAssignableTo(typeof(INotification)) || 
                type.IsAssignableTo(typeof(IUnitOfWork)) ||
                type.IsAssignableTo(typeof(IRefitClient)) ||
                type.Namespace == typeof(CorrelationIdMiddleware).Namespace ||
                type.Namespace == typeof(MultipleSourcesBindingSource).Namespace ||
                type.Namespace == typeof(ApplicationValidationMiddleware).Namespace ||
                type.IsAssignableTo(typeof(Attribute)) ||
                type.IsAssignableTo(typeof(IAuthorizationRequirement)) ||
                type == typeof(QueryExecuterOptions) ||
                type.IsAssignableTo(typeof(BackgroundService)) ||
                type.IsRecordType() ||
                type.IsAssignableTo(typeof(IHostedService)) ||
                type.Assembly == typeof(ObjectUtils).Assembly ||
                type.GetCustomAttribute<DependencyScannerIgnoreAttribute>() != null;

        }

        private static IEnumerable<Assembly> MakeSureAllAssembliesAreLoaded()
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => !x.IsDynamic && x.FullName.StartsWith(RootNameSpace)).ToList();
            var location=Directory.GetParent(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath).FullName;
            foreach (var assemblyFile in Directory.EnumerateFiles(location,$"{RootNameSpace}*.dll"))
            {
                if (!loadedAssemblies.Any(x => new Uri(x.Location).LocalPath.Equals(assemblyFile, StringComparison.OrdinalIgnoreCase)))
                    loadedAssemblies.Add(Assembly.LoadFrom(assemblyFile));
            }
            return loadedAssemblies;
        }



        public static IServiceCollection ScanAndRegisterDependencies(this IServiceCollection services)
        {
            //services.AddScoped(typeof(ICustomActivityInvoker<,>), typeof(CustomActivityInvoker<,>));
            services.AddScoped(typeof(ISimpleCrudRepository<>), typeof(SimpleCrudRepository<>));
            services.AddScoped(typeof(ISimpleCrudService<>), typeof(SimpleCrudService<>)); 

            var assemblies = MakeSureAllAssembliesAreLoaded();
            services.Scan(scan =>
            {
                scan.FromAssemblies(assemblies)
                    .AddClasses(classes => classes.Where(x => !IsExcludedType(x)))
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime();
            });
            return services;
        }
    }
}
