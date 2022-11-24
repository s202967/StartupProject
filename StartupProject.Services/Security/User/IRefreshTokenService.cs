using StartupProject.Core.Security.Identity;
using StartupProject.Core.ServiceResult;
using System.Threading.Tasks;

namespace StartupProject.Services.Security.User
{
    public interface IRefreshTokenService
    {
        /// <summary>
        /// Create refresh token
        /// </summary>
        Task<ServiceResult<RefreshToken>> CreateAsync(RefreshToken entity);

        /// <summary>
        /// Update refresh token
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ServiceResult<RefreshToken>> UpdateAsync(RefreshToken entity);

        /// <summary>
        /// Get refresh token by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        RefreshToken GetByUserName(string username, string ipAddress);

        /// <summary>
        /// Get refresh token by token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>

        ServiceResult<RefreshToken> GetByToken(string refreshToken, string ipAddress);

        /// <summary>
        /// Delete token
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ServiceResult<RefreshToken>> DeleteAsync(string username, string ipAddress);
    }
}
