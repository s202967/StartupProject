using System.Threading.Tasks;

namespace StartupProject.Core.Security.Identity
{
    public interface IApplicationSignInManager
    {
        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="isPersistent"></param>
        /// <param name="lockoutOnFailure"></param>
        /// <returns></returns>
        Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);

        /// <summary>
        /// Sign out
        /// </summary>
        /// <returns></returns>
        Task SignOutAsync();
    }
}
