using StartupProject.Core.Domain.Interfaces;
using StartupProject.Core.Security.Identity;
using StartupProject.EntityFramework.Repository;
using StartupProject.EntityFramework.Security.Identity;
using StartupProject.Services.Security.User;
using IRC.Services.Security.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StartupProject.DependencyResolution
{
    /// <summary>
    /// Configures security services
    /// </summary>
    public static class SecurityModule
    {
        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration"></param>
        public static void ConfigureSecurityModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserTokenService, UserService>();

            services.Register()
                .Repo<RefreshToken, RefreshTokenRepository>()
                .WithService<IRefreshTokenService, RefreshTokenService>();

            services.AddScoped<IRoleService, RoleService>();            
        }
    }
}
