using StartupProject.Core.Domain.DbEntity;
using StartupProject.Core.ServiceResult;

namespace StartupProject.Services.Common.Email
{
    /// <summary>
    /// Contains email service abstractions
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Gets email service provider
        /// </summary>
        IEmailSenderFactory GetSender { get; }

        /// <summary>
        /// Gets email content from db store
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        EmailContent GetEmailContentByType(int contentType);

        /// <summary>
        /// Checks local access
        /// </summary>
        /// <param name="to"></param>
        /// <param name="bcc"></param>
        /// <returns></returns>
        ServiceResult CanSendEmail(string to, string bcc);

        MailSetting GetMailSetting();
    }
}
