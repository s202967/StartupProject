using System.Threading.Tasks;

namespace StartupProject.Services.Security.User
{
    /// <summary>
    /// Contains service to generate and verify user token
    /// </summary>
    public interface IUserTokenService
    {
        /// <summary>
        /// Generate authentication token
        /// </summary>
        /// <param name="username">User name </param>
        /// <param name="purpose">Purpose:Value can be passwordless-auth to authenticate user without password</param>
        /// <returns></returns>
        Task<string> GeneratAsync(string username, string purpose);

        /// <summary>
        /// Verify user token generated to authenticate mobile user
        /// </summary>
        /// <param name="username">User Name</param>
        /// <param name="purpose">Purpose:Value can be passwordless-auth to authenticate user without password</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> VerifyAsync(string username, string purpose, string token);
    }
}
