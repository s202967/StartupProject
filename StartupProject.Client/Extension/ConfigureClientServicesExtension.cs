using StartupProject.Client.Features.Common.Factories;
using StartupProject.Client.Features.Security.User.Factories;
using Microsoft.Extensions.DependencyInjection;
using StartupProject.Client.Features.Employees.Factory;
//using StartupProject.Client.Extension.ClientServices;

namespace StartupProject.Client.Extension
{
    /// <summary>
    /// Represents extensions of IServiceCollection for factory classes
    /// </summary>
    public static class ConfigureClientServicesExtension
    {
        /// <summary>
        /// configure factory classes
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureClientServices(this IServiceCollection services)
        {
            //Attribute
            services.ConfigureAttributes();

            //Factory classes
            services.AddScoped(typeof(ICommonDtoFactory), typeof(CommonDtoFactory));
            services.AddScoped(typeof(IUserDtoFactory), typeof(UserDtoFactory));
            services.AddScoped(typeof(IEmployeeFactory), typeof(EmployeeFactory));

        }
    }
}
