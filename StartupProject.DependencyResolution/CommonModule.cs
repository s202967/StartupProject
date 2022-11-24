using StartupProject.Core;
using StartupProject.Core.Caching;
using StartupProject.Core.Domain.DbEntity;
using StartupProject.Core.Domain.Interfaces;
using StartupProject.Core.Infrastructure.DataAccess;
using StartupProject.Core.Infrastructure.DependencyManagement;
using StartupProject.Core.Infrastructure.Localization;
using StartupProject.Core.Security.UserActivity;
using StartupProject.EntityFramework.EntityFramework;

using StartupProject.Services.Common;
using StartupProject.Services.Common.Email;
using StartupProject.Services.Common.Localization;

using StartupProject.Services.Helpers;
using StartupProject.Services.Helpers.CustomDate;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StartupProject.Services.Common.Settings;
using StartupProject.EntityFramework.Repository;
using StartupProject.EntityFramework.Provider;
using StartupProject.Services.Common.Service;

namespace StartupProject.DependencyResolution
{
    /// <summary>
    ///configure common services
    /// </summary>
    public static class CommonModule
    {
        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration"></param>
        public static void ConfigureCommonServiceModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(ISessionProvider), typeof(SessionProvider));
            services.AddScoped(typeof(IUow), typeof(UnitOfWork));
            services.AddScoped(typeof(ITransactionManager), typeof(TransactionManager));
            services.AddScoped(typeof(IStaticCacheManager), typeof(MemoryCacheManager));
            services.AddScoped(typeof(IWorkContext), typeof(WorkContext));
            services.AddScoped(typeof(IClientInfoProvider), typeof(HttpContextClientInfoProvider));
            services.AddScoped(typeof(IDateFactory), typeof(DateFactory));

            var awsSettingSection = configuration.GetSection("AWS");
            services.Configure<AwsSettings>(awsSettingSection);

            services.Register().Repo<LocaleStringResource, LocaleStringResourceRepository>().
            WithService<ILocalizationService, LocalizationService>();


            //Email     
            services.Register().Repo<EmailContent, EmailContentRepository>().
            WithService<IEmailService, EmailService>();

            //Employee
            services.Register().Repo<Employee, EmployeeRepository>().
                WithService<IEmployeeService, EmployeeService>();


            //Mail settings
            services.AddScoped(typeof(IMailSettingRepository), typeof(MailSettingRepository));
            services.AddScoped(typeof(IMailSettingService), typeof(MailSettingService));

            services.Register().Repo<CheckList, CheckListRepository>();
            services.Register().Repo<Component, ComponentRepository>();
            services.Register().Repo<Section, SectionRepository>();
            services.Register().Repo<Template, TemplateRepository>();
            services.AddScoped(typeof(ICommonService), typeof(CommonService));

        }
    }
}
