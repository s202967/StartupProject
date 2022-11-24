using StartupProject.Core.Infrastructure.Extensions;
using StartupProject.Core.ServiceResult;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace StartupProject.Client.Middleware
{
    /// <summary>
    /// Global exception handler
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// configure 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="logger"></param>
        /// <param name="env"></param>
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger<Startup> logger, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        //var loggerFactory = EngineContext.Current.Resolve<ILoggerFactory>();
                        //var logger = loggerFactory.CreateLogger("ExceptionMiddlewareExtensions");
                        //  var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        var message = new List<string>();
                        if (env.IsDevelopment())
                        {
                            message = contextFeature.Error.Message?.ToStringList();
                        }
                        else
                        {
                            message = "We are unable to process your request at this time. Please try again later, and if the problem persists, contact your system administrator.".ToStringList();
                        }

                        var contractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new CamelCaseNamingStrategy()
                        };

                        string jsonObject = JsonConvert.SerializeObject(new ServiceResult(false, message, MessageType.Danger)
                        , new JsonSerializerSettings
                        {
                            //ContractResolver = contractResolver,
                            Formatting = Formatting.Indented
                        });

                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        await context.Response.WriteAsync(jsonObject, Encoding.UTF8);
                        return;
                    }
                });
            });
        }
    }
}
