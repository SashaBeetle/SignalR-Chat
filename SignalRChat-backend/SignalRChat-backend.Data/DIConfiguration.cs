using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignalRChat_backend.Data.Interfaces;
using SignalRChat_backend.Data.Services;

namespace SignalRChat_backend.Data
{
    public static class DIConfiguration
    {
        private static void RegisterDatabaseDependencies(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddDbContext<SignalRChatDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("SignalRChatDatabase")));
        }
        private static void RegisterServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IDbEntityService<>), typeof(DbEntityService<>));
            services.AddScoped<IChatDbService, ChatDbService>();
            services.AddScoped<IUserDbService, UserDbService>();
        }
        public static void RegisterDataDependencies(this IServiceCollection services, IConfigurationRoot configuration)
        {
            RegisterDatabaseDependencies(services, configuration);
            RegisterServiceDependencies(services);
        }
    }
}
