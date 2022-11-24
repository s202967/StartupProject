using StartupProject.Core.Security.Identity;
using StartupProject.EntityFramework.EntityFramework;
using StartupProject.EntityFramework.Security.Identity;
using StartupProject.EntityFramework.Security.Identity.Models;
using StartupProject.Services.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using StartupProject.Services.Common.Service;
using StartupProject.Core.Infrastructure.DataAccess;

namespace StartupProject.DependencyResolution
{
    /// <summary>
    /// Represents object for the configuring authentication middleware on application startup
    /// </summary>
    public class AuthenticationStartup
    {
        private void ConfigureDbConnection(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DbConnectionString"),
                   sqlOptions =>
                   {
                       sqlOptions.MigrationsAssembly("StartupProject.EntityFramework");
                       //sqlOptions.EnableRetryOnFailure();
                   }));
        }

        private void ConfigureIdentityOptions(IServiceCollection services, IdentityConfig idenityOption)
        {
            services.AddIdentity<ApplicationIdentityUser, ApplicationIdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<SignInManager<ApplicationIdentityUser>, ApplicationSignInManager>();
            services.AddScoped<UserManager<ApplicationIdentityUser>, ApplicationUserManager>();
            services.AddScoped<RoleManager<ApplicationIdentityRole>, ApplicationRoleManager>();
            services.AddScoped<IApplicationUserManager, ApplicationUserManager>();
            services.AddScoped<IApplicationSignInManager, ApplicationSignInManager>();
            services.AddScoped<IApplicationRoleManager, ApplicationRoleManager>();
            //services.AddTransient<IEmployeeService, EmployeeService>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = idenityOption.Password.RequireDigit;
                options.Password.RequiredLength = idenityOption.Password.RequiredLength;
                options.Password.RequireNonAlphanumeric = idenityOption.Password.RequireNonAlphanumeric;
                options.Password.RequireUppercase = idenityOption.Password.RequireUppercase;
                options.Password.RequireLowercase = idenityOption.Password.RequireLowercase;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(idenityOption.Lockout.DefaultLockoutTimeSpanInMins);
                options.Lockout.MaxFailedAccessAttempts = idenityOption.Lockout.MaxFailedAccessAttempts;
                options.Lockout.AllowedForNewUsers = idenityOption.Lockout.AllowedForNewUsers;

                // User settings
                options.User.RequireUniqueEmail = idenityOption.User.RequireUniqueEmail;
            });
        }

        private void ConfigureJwtBearer(IServiceCollection services, IConfiguration configuration, IdentityConfig idenityOption)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(idenityOption.ApiToken.SecretKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false, // Do not validate lifetime here
                };
            });
        }

        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration root of the application</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            ConfigureDbConnection(services, configuration);
            //add authentication
            var identitySection = configuration.GetSection("IdentityOptions");
            services.Configure<IdentityConfig>(identitySection);
            var identityOption = identitySection.Get<IdentityConfig>();
            ConfigureIdentityOptions(services, identityOption);
            ConfigureJwtBearer(services, configuration, identityOption); 
        }

        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {
            //configure authentication
            //application.UseNopAuthentication();

            ////set request culture
            //application.UseCulture();
        }

        /// <summary>
        /// Gets order of this startup configuration implementation
        /// </summary>
        public int Order
        {
            //authentication should be loaded before MVC
            get { return 500; }
        }
    }
}
