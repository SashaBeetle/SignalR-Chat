using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignalRChat_backend.Services.Interfaces;
using SignalRChat_backend.Services.Services;

namespace SignalRChat_backend.Services
{
    public static class DIConfiguration
    {
        private static void RegisterServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMessageService, MessageService>();
        }
        public static void RegisterServiceDependencies(this IServiceCollection services, IConfigurationRoot configuration)
        {
            RegisterServiceDependencies(services);
        }
    }
}
