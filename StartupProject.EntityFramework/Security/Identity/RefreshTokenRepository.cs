using StartupProject.Core.Security.Identity;
using StartupProject.EntityFramework.EntityFramework;

namespace StartupProject.EntityFramework.Security.Identity
{
    public class RefreshTokenRepository : Repository<RefreshToken>
    {
        public RefreshTokenRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
