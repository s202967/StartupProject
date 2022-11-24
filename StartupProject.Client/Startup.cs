using StartupProject.Client.Extension;
using StartupProject.Client.Middleware;
using StartupProject.Core.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using StartupProject.Client.Features.Employees.Factory;

namespace StartupProject.Client
{
    /// <summary>
    /// Includes a ConfigureServices method to configure the app's services
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// providers configuration data
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureClientStartupServices(Configuration);
            services.ConfigureApplicationServices(Configuration);
            services.ConfigureClientServices();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            app.ConfigureRequestPipeline();

            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.None,
            };
            app.UseCookiePolicy(cookiePolicyOptions);

            if (env.IsDevelopment())
            {
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();
                app.UseStaticFiles();
                //app.UseSpaStaticFiles();

                app.UseSpaStaticFiles(new StaticFileOptions { RequestPath = "/clientapp/build" });

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Journal System API V1");
                    c.RoutePrefix = "swagger";
                    c.DefaultModelExpandDepth(2);
                    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                });

                app.UseDeveloperExceptionPage();
            }

            app.ConfigureExceptionHandler(logger, env);
            app.UseRouting();
            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            //Require HTTPS
            //  var httpsRedirectionEnabled
            bool.TryParse(Configuration["AppSettings:HttpsRedirectionEnabled"], out bool httpsRedirectionEnabled);
            if (httpsRedirectionEnabled)
            {
                app.UseHttpsRedirection();
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
