using StartupProject.Core.ServiceResult;
using System.Threading.Tasks;

namespace StartupProject.Services.Common.Email
{
    public interface IEmailSenderFactory
    {
        /// <summary>
        /// Send email to receiver's email Address
        /// </summary>
        /// <param name="subject"> Subject</param>
        /// <param name="senderAddress">Sender's email address</param>
        /// <param name="receiverAddress">Receivers email address</param>
        /// <param name="htmlBody">HTML body</param>
        /// <param name="textBody">Text body</param>
        /// <returns></returns>
        Task<ServiceResult> SendAsync(string subject, string receiverAddress, string htmlBody, string bcc = "", string textBody = "");
    }
}
