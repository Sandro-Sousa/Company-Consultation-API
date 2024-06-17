

using Repository.API.Adapter;
using Repository.API.Interfaces;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Interfaces;
using Service.Services;

namespace Company_Consultation.WebAPI.Extensions
{
    public static class DependencyContainer
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            // Repository
            builder.Services.AddTransient<IEmailConfigurationRepository, EmailConfigurationRepository>();
            builder.Services.AddTransient<ILoginRepository, LoginRepository>();
            builder.Services.AddTransient<IOrderRepository, OrderRepository>();
            builder.Services.AddTransient<IExternalApiReceitawsAdapter, ExternalApiReceitawsAdapter>();

            // Services
            builder.Services.AddTransient<ILoginService, LoginService>();
            builder.Services.AddTransient<IEmailConfigurationService, EmailConfigurationService>();
            builder.Services.AddTransient<IOrderService, OrderService>();
        }   
    }
}
