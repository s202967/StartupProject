using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace StartupProject.Client.Extension
{
    /// <summary>
    /// Represents extensions of IApplicationBuilder
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configure the application HTTP request pipeline
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void ConfigureRequestPipeline(this IApplicationBuilder application)
        {
            // application.ConfigureAuth();
        }

        /// <summary>
        /// Configure auth pipeline
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureAuth(this IApplicationBuilder app)
        {
            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.None,
            };
            app.UseCookiePolicy(cookiePolicyOptions);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();
        }
    }
}
