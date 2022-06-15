using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EMarket.Core.Application.Interfaces.Services;
using EMarket.Core.Application.Services;

namespace EMarket.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            #region Services
            services.AddTransient<IAdvertisementService, AdvertisementService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IUserService, UserService>();
            #endregion
        }
    }
}
