using StartupProject.Core.Domain.DbEntity;
using StartupProject.Core.Domain.Interfaces;
using System.Linq;

namespace StartupProject.Services.Common.Email
{
    public class MailSettingService : IMailSettingService
    {
        private readonly IMailSettingRepository _mailSettingRepository;

        public MailSettingService(IMailSettingRepository mailSettingRepository)
        {
            _mailSettingRepository = mailSettingRepository;
        }

        public MailSetting GetMailSetting()
        {
            return _mailSettingRepository.Table.FirstOrDefault();
        }
    }
}
