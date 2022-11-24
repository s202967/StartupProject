using Amazon.SimpleEmail;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Globalization;
using StartupProject.Core.Domain.DbEntity;
using StartupProject.Core.Infrastructure.DataAccess;
using StartupProject.Core.Security.UserActivity;
using StartupProject.Services.Common.Localization;
using StartupProject.Core.ServiceResult;
using StartupProject.Core.Infrastructure.Extensions;

namespace StartupProject.Services.Common.Email
{
    /// <summary>
    /// Contains email services 
    /// </summary>
    public class EmailService : IEmailService
    {

        #region Ctor/fields

        private readonly IRepository<EmailContent> _emailContentRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IClientInfoProvider _clientInfoProvider;
        private readonly ILocalizationService _localizationService;
        private readonly ILogger _logger;
        private readonly AppSettings _appSetting;
        private readonly IAmazonSimpleEmailService _sesClient;
        private readonly IMailSettingService _mailSettingService;
        private readonly AwsSettings _awsSettings;

        public EmailService
        (
            IRepository<EmailContent> emailContentRepository,
            IOptions<AppSettings> appSettingOptions,
            IOptions<AwsSettings> awsSettings,
            IHttpContextAccessor contextAccessor,
            IClientInfoProvider clientInfoProvider,
            ILocalizationService localizationService,
            ILoggerFactory loggerFactory,
            IAmazonSimpleEmailService sesClient,
            IMailSettingService mailSettingService
        )
        {
            _contextAccessor = contextAccessor;
            _clientInfoProvider = clientInfoProvider;
            _emailContentRepository = emailContentRepository;
            _appSetting = appSettingOptions.Value;
            _localizationService = localizationService;
            _logger = loggerFactory.CreateLogger("AwsEmailService");
            _sesClient = sesClient;
            _mailSettingService = mailSettingService;
            _awsSettings = awsSettings.Value;
        }

        #endregion

        /// <summary>
        /// Gets email content from db store
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public EmailContent GetEmailContentByType(  int contentType)
        {
            return _emailContentRepository.Get(x => x.Type == contentType);
        }

        public IEmailSenderFactory GetSender
        {
            get
            {
                //Firstly, if SMTP setting found on db then use from there
                if (GetMailSetting() != default)
                    return new SmtpEmailService(CanSendEmail, _localizationService, _logger, GetMailSetting());
                else
                    return new EmailConfigNotFoundService(_localizationService, _logger);
            }
        }

        /// <summary>
        /// Checks local access.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="bcc"></param>
        /// <returns></returns>
        public ServiceResult CanSendEmail(string to, string bcc)
        {
            return new ServiceResult(true);
        }

        public MailSetting GetMailSetting()
        {
            return _mailSettingService.GetMailSetting();
        }
    }
}
