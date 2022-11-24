using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StartupProject.Core.Infrastructure.DependencyManagement
{
    /// <summary>
    /// Dependency registrar interface
    /// </summary>
    public interface IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        void Register(IServiceCollection services, IConfiguration configuration);
    }
}
