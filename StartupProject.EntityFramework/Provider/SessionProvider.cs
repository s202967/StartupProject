using StartupProject.Core.Infrastructure.DependencyManagement;
using StartupProject.EntityFramework.EntityFramework;
using System;

namespace StartupProject.EntityFramework.Provider
{
    public class SessionProvider : ISessionProvider
    {
        private readonly ApplicationDbContext _dbContext;

        public SessionProvider(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SetUserId(Guid currentUserId)
        {
            _dbContext.UserId = currentUserId;
        }
    }
}
