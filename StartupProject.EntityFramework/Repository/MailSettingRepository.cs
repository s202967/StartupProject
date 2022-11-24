using StartupProject.Core.Domain.DbEntity;
using StartupProject.Core.Domain.Interfaces;
using StartupProject.EntityFramework.EntityFramework;

namespace StartupProject.EntityFramework.Repository
{
    public class MailSettingRepository : Repository<MailSetting>, IMailSettingRepository
    {
        public MailSettingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
