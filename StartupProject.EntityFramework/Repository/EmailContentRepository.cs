using StartupProject.Core.Domain.DbEntity;
using StartupProject.EntityFramework.EntityFramework;

namespace StartupProject.EntityFramework.Repository
{
    public class EmailContentRepository : Repository<EmailContent>
    {
        public EmailContentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
