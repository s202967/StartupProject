using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using StartupProject.Core.Infrastructure.Extensions;
using StartupProject.Core.ServiceResult;
using StartupProject.Services.Common.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StartupProject.Services.Common.Email
{
    /// <summary>
    /// Credential 
    /// </summary>
    public class AwsEmailService : IEmailSenderFactory
    {
        private readonly Func<string, string, ServiceResult> _checkIfClientIsLocal;
        private readonly ILocalizationService _localizationService;
        private readonly ILogger _logger;
        private readonly IAmazonSimpleEmailService _sesClient;
        private readonly AwsSettings _awsSettings;

        public AwsEmailService(Func<string, string, ServiceResult> action,
            ILocalizationService localizationService, ILogger logger,
            IAmazonSimpleEmailService sesClient, AwsSettings awsSettings)
        {
            _checkIfClientIsLocal = action;
            _localizationService = localizationService;
            _logger = logger;
            _sesClient = sesClient;
            _awsSettings = awsSettings;
        }

        /// <summary>
        /// Send email to receiver's email Address
        /// </summary>
        /// <param name="subject"> Subject</param>       
        /// <param name="receiverAddress">Receivers email address</param>
        /// <param name="htmlBody">HTML body</param>
        /// <param name="textBody">Text body</param>
        /// <returns></returns>
        public async Task<ServiceResult> SendAsync(string subject, string receiverAddress, string htmlBody, string bcc = "", string textBody = "")
        {
            var resp = _checkIfClientIsLocal(receiverAddress, bcc);
            if (!resp.Status)
                return resp;

            var sendRequest = new SendEmailRequest
            {
                Source = _awsSettings.FromEmail,
                Destination = new Destination
                {
                    ToAddresses = receiverAddress.Split(',').ToList()
                    //new List<string> { receiverAddress }
                },
                Message = new Message
                {
                    Subject = new Content(subject),
                    Body = new Body
                    {
                        Html = new Content
                        {
                            Charset = "UTF-8",
                            Data = htmlBody
                        },
                        Text = new Content
                        {
                            Charset = "UTF-8",
                            Data = textBody
                        }

                    }
                },
                // If you are not using a configuration set, comment
                // or remove the following line 
                // ConfigurationSetName = configSet
            };

            try
            {
                var response = await _sesClient.SendEmailAsync(sendRequest).ConfigureAwait(false);
                return new ServiceResult(true)
                {
                    Message = _localizationService.GetResource("User.PasswordResetLink.SentSuccessfully").ToStringList()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ServiceResult(false, _localizationService.GetResource("User.Email.Send.Failed").ToStringList(), MessageType.Danger);

            }
        }

    }
}
