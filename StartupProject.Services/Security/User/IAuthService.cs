using StartupProject.Core.ServiceResult;
using System.Threading.Tasks;

namespace StartupProject.Services.Security.User
{
    /// <summary>
    /// Generate and validate tokens, Refresh access token
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Sign In
        /// </summary>
        /// <param name="username">User Name</param>
        /// <param name="password"> Password: Value can be null in case of switch user</param>
        /// <param name="isPersistent"></param>
        /// <returns></returns>
        Task<ServiceResult<SignInResponse>> SignIn(string username, string password, bool isPersistent);

        /// <summary>
        /// Refresh token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<ServiceResult<SignInResponse>> RefreshTokenAsync(string refreshToken);

        /// <summary>
        /// Sign out
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult> SignOutAsync();
    }
}
