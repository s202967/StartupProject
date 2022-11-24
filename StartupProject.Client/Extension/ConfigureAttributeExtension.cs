using StartupProject.Client.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace StartupProject.Client.Extension
{
    /// <summary>
    /// Represents extensions of IServiceCollection for attribute registration
    /// </summary>
    public static class ConfigureAttributeExtension
    {
        /// <summary>
        /// configure attributes
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureAttributes(this IServiceCollection services)
        {
            services.AddScoped<ValidateDtoAttribute>();
            services.AddScoped<UnitOfWorkAttribute>();
        }
    }
}
