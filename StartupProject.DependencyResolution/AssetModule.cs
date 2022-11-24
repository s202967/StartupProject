using StartupProject.Core.Domain.Interfaces;
using StartupProject.EntityFramework.Repository;
using StartupProject.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StartupProject.DependencyResolution
{
    /// <summary>
    ///configure asset services
    /// </summary>
    public static class AssetModule
    {
        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration"></param>
        public static void ConfigureAssetServiceModule(this IServiceCollection services, IConfiguration configuration)
        {
            ////Asset
            //services.AddScoped(typeof(IAssetRepository), typeof(AssetRepository));
            //services.AddScoped(typeof(IAssetService), typeof(AssetService));

            ////Asset Image
            //services.AddScoped(typeof(IAssetImageRepository), typeof(AssetImageRepository));
            //services.AddScoped(typeof(IAssetImageService), typeof(AssetImageService));

            
        }
    }
}
