using StartupProject.Core.Domain.DbEntity;

namespace StartupProject.Services.Common.Email
{
    public interface IMailSettingService
    {
        MailSetting GetMailSetting();
    }
}
