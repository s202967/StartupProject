using StartupProject.Core.Domain.DbEntity;
using StartupProject.Core.Infrastructure.Extensions;
using StartupProject.Core.ServiceResult;
using StartupProject.Services.Common.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace StartupProject.Services.Common.Email
{
    /// <summary>
    /// SMTP client to send email
    /// </summary>
    public class SmtpEmailService : IEmailSenderFactory
    {
        private readonly Func<string, string, ServiceResult> _checkIfClientIsLocal;
        private readonly ILocalizationService _localizationService;
        private readonly ILogger _logger;
        private readonly MailSetting _mailSetting;

        public SmtpEmailService(Func<string, string, ServiceResult> action,
            ILocalizationService localizationService, ILogger logger, MailSetting mailSetting)
        {
            _checkIfClientIsLocal = action;
            _localizationService = localizationService;
            _logger = logger;
            _mailSetting = mailSetting;
        }

        /// <summary>
        /// Send email to receiver's email Address
        /// </summary>
        /// <param name="subject">Subject</param>
        /// <param name="senderAddress">Sender's email address</param>
        /// <param name="receiverAddress">Receivers email address</param>
        /// <param name="htmlBody">HTML body</param>
        /// <param name="bcc">BCC</param>
        /// <param name="textBody">Text body</param>
        /// <returns></returns>
        public async Task<ServiceResult> SendAsync(string subject, string receiverAddress, string htmlBody, string bcc = "", string textBody = "")
        {
            string mailState = "state";
            var resp = _checkIfClientIsLocal(receiverAddress, bcc);
            if (!resp.Status)
                return resp;

            using (SmtpClient client = new SmtpClient(_mailSetting.HostName, _mailSetting.Port.Value))
            {
                if (!string.IsNullOrEmpty(_mailSetting.UserName))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_mailSetting.UserName, _mailSetting.Password);
                }

                client.EnableSsl = _mailSetting.EnableSsl;

                MailAddress from = new MailAddress(_mailSetting.FromEmail, subject); //MailAddress(string address, string displayName);

                MailMessage mailMessage = new MailMessage
                {
                    From = from //Sets the from address for this email message
                };

                foreach (var receiver in receiverAddress.Split(','))
                {
                    mailMessage.To.Add(new MailAddress(receiver)); //Add multiple recipients of this email message
                }

                if (!string.IsNullOrEmpty(bcc))
                    mailMessage.Bcc.Add(bcc); //Add the blind carbon copy (BCC) recipients for this email message.

                mailMessage.IsBodyHtml = true;

                mailMessage.Subject = subject;
                mailMessage.Body = htmlBody;

                try
                {
                    await client.SendMailAsync(mailMessage).ConfigureAwait(false);
                    //client.SendAsync(mailMessage,mailState);
                    return new ServiceResult(true)
                    {
                        Message = _localizationService.GetResource("Email.Sent.Success").ToStringList()
                    };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return new ServiceResult(false, _localizationService.GetResource("Email.Sent.Failure").ToStringList(), MessageType.Danger);
                }
            }
        }

    }
}
