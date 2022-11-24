using StartupProject.Core.Infrastructure.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace StartupProject.DependencyResolution
{
    public static class ServiceRegistrationExtension
    {
        /// <summary>
        /// Add the services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static RegistrationBuilder Register(this IServiceCollection services)
            => new RegistrationBuilder(services);
    }

    /// <summary>
    /// Configure services and repositories
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RegistrationBuilder
    {
        private readonly IServiceCollection _services;

        public RegistrationBuilder(IServiceCollection services)
        {
            _services = services;
        }

        /// <summary>
        /// Register the service
        /// </summary>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public RegistrationBuilder WithService<TInterface, TService>(ServiceLifetime lifetime = ServiceLifetime.Scoped) where TService : class where TInterface : class
        {
            _services.Add(ServiceDescriptor.Describe(typeof(TInterface), typeof(TService), lifetime));
            return this;
        }

        /// <summary>
        /// Register the prepository
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public RegistrationBuilder Repo<TEntity, TRepo>(ServiceLifetime lifetime = ServiceLifetime.Scoped) where TEntity : class where TRepo : class
        {
            _services.Add(ServiceDescriptor.Describe(typeof(IRepository<TEntity>), typeof(TRepo), lifetime));
            return this;
        }
    }
}
