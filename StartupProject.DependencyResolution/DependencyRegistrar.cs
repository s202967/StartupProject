using StartupProject.Core.Infrastructure.DependencyManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace StartupProject.DependencyResolution
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public virtual void Register(IServiceCollection services, IConfiguration configuration)
        {
            //auth
            var authenticationStartup = new AuthenticationStartup();
            authenticationStartup.ConfigureServices(services, configuration);

            //Security service
            services.ConfigureSecurityModule(configuration);

            //Common service
            services.ConfigureCommonServiceModule(configuration);

        }

        /// <summary>
        /// Gets order of this dependency registrar implementation
        /// </summary>
        public int Order
        {
            get { return 0; }
        }
    }
}
