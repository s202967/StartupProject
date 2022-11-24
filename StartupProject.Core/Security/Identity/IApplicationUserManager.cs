using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StartupProject.Core.Security.Identity
{
    public interface IApplicationUserManager : IDisposable
    {
        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<ApplicationIdentityResult> CreateAsync(AppUser user);

        /// <summary>
        /// Update existing user details
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        Task<ApplicationIdentityResult> UpdateAsync(AppUser appUser);

        /// <summary>
        /// Deletes user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ApplicationIdentityResult> DeleteUserAsync(string userId);

        /// <summary>
        /// Find user by user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<AppUser> FindByUserNameAsync(string userName);

        /// <summary>
        /// Find user by user id
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<AppUser> FindByUserIdAsync(string userId);

        /// <summary>
        /// Find by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<AppUser> FindByEmailAsync(string email);

        /// <summary>
        /// Verify user token generated to authenticate mobile user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="purpose"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> VerifyUserTokenAsync(string username, string purpose, string token);

        /// <summary>
        /// Generate authentication token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="purpose"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<string> GenerateUserTokenAsync(string username, string purpose);

        /// <summary>
        /// Gets lockout end date time
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<DateTime> GetLockEndLocalDateTimeAsync(string username);

        /// <summary>
        /// Generate reset password token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<string> GeneratePasswordResetTokenAsync(string userId);

        /// <summary>
        /// Reset user password with token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task<ApplicationIdentityResult> ResetPasswordAsync(string userId, string token, string newPassword);

        /// <summary>
        /// Reset password without token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task<ApplicationIdentityResult> ResetPasswordAsync(string userId, string newPassword);

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currentPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task<ApplicationIdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AppUser>> GetAllUsersAsync();

        /// <summary>
        /// update security timestamp
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ApplicationIdentityResult> UpdateSecurityStampAsync(string userId);
    }
}