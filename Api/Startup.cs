using Microsoft.Extensions.DependencyInjection;
using TestApp.Application.Security;
using TestApp.Application.Queries.UserManagement;
using TestApp.Application.Queries;
using TestApp.Application.Services.UserManagement;
using TestApp.Application.Queries; 

namespace TestApp.Api
{
    public class Startup : ModuleInitializer
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env) : base(configuration, env)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            //services.AddDbContext<DbContext, WarehousesContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Application")));
            //services.AddNotificationHandlersFrom<ActivityExecutedEventHandler>();
            //services.AddNotificationHandlersFrom<WorkflowCompletedEventHandler>();
            //services.WithJavaScriptOptions(options => options.EnableConfigurationAccess = true);
            //services.AddBookmarkProvider<UserTaskBookmarkProvider>();
            //services.AddBookmarkProvider<CommitteeTaskBookmarkProvider>();
            services.AddTransient<AuthHeaderHandler>();
            //services.AddSingleton(new LocalizationServiceOptions("INV:"));
            //services.AddScoped<ICurrentUser,  CurrentUser>();
            //services.AddScoped<IUserQueries, UserQueries>();
            //services.AddScoped<IQueryExecuter, QueryExecuter>();
            //services.AddScoped<IInboxQueries, InboxQueries>();

        }

        protected override void ConfigureOptions(IServiceCollection services)
        {
            base.ConfigureOptions(services);
         }
       
    }
}
