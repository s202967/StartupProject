using System;

namespace StartupProject.Core.Infrastructure.DependencyManagement
{
    public interface ISessionProvider
    {
        void SetUserId(Guid currentUserId);
    }
}
