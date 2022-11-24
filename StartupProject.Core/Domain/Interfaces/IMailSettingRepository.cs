using StartupProject.Core.Domain.DbEntity;
using StartupProject.Core.Infrastructure.DataAccess;

namespace StartupProject.Core.Domain.Interfaces
{
    public interface IMailSettingRepository : IRepository<MailSetting>
    {
    }
}
