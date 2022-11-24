using Amazon.SimpleEmail;
using StartupProject.Client.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Reflection;

namespace StartupProject.Client.Extension
{
    /// <summary>
    /// Represents extensions of IServiceCollection for common services
    /// </summary>
    public static class ClientConfigurationExtension
    {
        /// <summary>
        /// configure factories
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureClientStartupServices(this IServiceCollection services, IConfiguration configuration)
        {
            AwsSesStartup(services, configuration);
            MvcStartup(services);
         
            ConfigureSwagger(services);
        }

        /// <summary>
        /// AWS simple email service registration
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        private static void AwsSesStartup(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultAWSOptions(configuration.GetAWSOptions());
            services.AddAWSService<IAmazonSimpleEmailService>();
        }

       

        /// <summary>
        /// MVC services
        /// </summary>
        /// <param name="services"></param>
        private static void MvcStartup(IServiceCollection services)
        {
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                //options.SuppressConsumesConstraintForFormFileParameters = true;
                //options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
                options.SuppressMapClientErrors = true;
                //    options.ClientErrorMapping[404].Link =
            });
            services.AddControllers();

            services.AddMvc(option =>
            {
                //filter for user authentication
                option.Filters.Add(typeof(AuthenticationFilter));
                //*
                option.EnableEndpointRouting = false;
            })
               //*Json.Net is not longer supported, .NET Core 3 they change little bit the JSON politics.
               //handles exception -object cyclce detected issue of core 3.X
               .AddNewtonsoftJson(opt =>
               {
                   opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                   opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
               }
               );
        }

        /// <summary>
        ///  Configure Swagger
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigureSwagger(IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                //https://thecodebuzz.com/jwt-authorization-token-swagger-open-api-asp-net-core-3-0/
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "REST services for StartupProject.",
                    Description = "Through this API you can access services.",
                    Contact = new OpenApiContact()
                    {
                        Email = "dev@dev.com",
                        Name = "tech.",
                        Url = new Uri("https://abc.com/")
                    }
                });
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                c.IncludeXmlComments(xmlCommentsFullPath);
            });
        }

    }
}
