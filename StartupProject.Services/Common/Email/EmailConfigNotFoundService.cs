using StartupProject.Core.Infrastructure.Extensions;
using StartupProject.Core.ServiceResult;
using StartupProject.Services.Common.Localization;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace StartupProject.Services.Common.Email
{
    public class EmailConfigNotFoundService : IEmailSenderFactory
    {
        private readonly ILocalizationService _localizationService;
        private readonly ILogger _logger;

        public EmailConfigNotFoundService(ILocalizationService localizationService, ILogger logger)
        {
            _localizationService = localizationService;
            _logger = logger;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<ServiceResult> SendAsync(string subject, string receiverAddress, string htmlBody, string bcc = "", string textBody = "")
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            _logger.LogError(_localizationService.GetResource("Email.Config.NotFound"));
            return new ServiceResult(false, _localizationService.GetResource("Email.Config.NotFound").ToStringList(), MessageType.Danger);
        }
    }
}
