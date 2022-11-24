using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using StartupProject.EntityFramework.EFConfiguration;
using StartupProject.Services.Common.Localization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace StartupProject.Client
{
    /// <summary>
    /// entry point to the application
    /// </summary>
    public class Program
    {
        /// <summary>
        /// main method
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main");
                var host = CreateHostBuilder(args).Build();
                //host.SeedData();

                using (var serviceScope = host.Services.CreateScope())
                {
                    var services = serviceScope.ServiceProvider;

                    try
                    {
                        var serviceContext = services.GetRequiredService<ILocalizationService>();
                        serviceContext.ImportResourcesFromXml();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, ex.Message);
                    }
                }

                await host.RunAsync();
            }
            catch (Exception exception)
            {
                //NLog: catch setup errors
                logger.Error(exception, $"Stopped program because of exception:{ exception.Message}");
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        /// <summary>
        ///  create and configure a builder object
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args).
        ConfigureAppConfiguration(EntityFrameworkExtensions.AddDbConfiguration)
       .ConfigureWebHostDefaults(webBuilder =>
       {
          
             
           webBuilder.UseIISIntegration();
           webBuilder.UseStartup<Startup>();
       })
       .ConfigureLogging(logging =>
       {
           logging.ClearProviders();
           logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
       })
       .UseNLog();  // NLog: Setup NLog for Dependency injection
    }
}
