using StartupProject.Core.Infrastructure.Localization;
using StartupProject.EntityFramework.EntityFramework;

namespace StartupProject.EntityFramework.Repository
{
    public class LocaleStringResourceRepository : Repository<LocaleStringResource>
    {
        public LocaleStringResourceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
