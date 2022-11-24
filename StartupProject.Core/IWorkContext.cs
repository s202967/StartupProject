using StartupProject.Core.Security.Identity.User;

namespace StartupProject.Core
{
    public interface IWorkContext
    {
        /// <summary>
        /// Gets the current user
        /// </summary>
        UserCore CurrentUser { get; }
    }
}
