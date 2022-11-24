using StartupProject.Core.Domain;
using StartupProject.Core.Domain.SpEntity;
using StartupProject.Core.Security.Identity;
using StartupProject.Core.Security.Identity.User;
using StartupProject.Core.ServiceResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StartupProject.Services.Security.User
{
    /// <summary>
    /// User service abstraction
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Get users
        /// </summary>
        /// <returns></returns>
        Task<IList<AppUser>> GetUsersAsync();

        /// <summary>
        /// Ger user list
        /// </summary>
        /// <param name="userName">User name filter</param>
        /// <param name="status">True:active users, False:disabled users</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        Task<IEnumerable<UserListProcResult>> GetUserListAsync(string userName, bool status, int pageIndex, int pageSize);

        /// <summary>
        /// Creates user
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ServiceResult<AppUser>> CreateAsync(AppUser entity);

        /// <summary>
        /// Update existing user
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ServiceResult<AppUser>> UpdateAsync(AppUser entity);

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ServiceResult> DeleteAsync(string userId);

        /// <summary>
        /// Gets user by user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<ServiceResult<AppUser>> GetUserByUserNameAsync(string userName);

        /// <summary>
        /// Gets user by user name
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ServiceResult<AppUser>> GetUserByUserIdAsync(string userId);

        /// <summary>
        /// Gets detailed information of user 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<UserCore> GetUserDetailsByUserNameAsync(string userName);

        /// <summary>
        /// Send user name to email
        /// </summary>                                                                                                                                                                                                                                                                                              q
        /// <param name="email">Receiver's email</param>
        /// <returns></returns>
        Task<ServiceResult> SendUserNameToEmailAsync(string email);

        /// <summary>
        /// Send  password reset link to the respective email address
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<ServiceResult> SendResetPswLinkInEmailAsync(string email);

        /// <summary>
        /// Reset password
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="code">Token</param>
        /// <param name="password"> Password</param>
        /// <returns>Service result</returns>
        Task<ServiceResult> ResetPasswordAsync(string userId, string code, string password);

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="currentPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task<ServiceResult> ChangePasswordAsync(string username, string currentPassword, string newPassword);

        /// <summary>
        /// Change other User Password from Admin account
        /// </summary>
        /// <param name="username"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task<ServiceResult> ChangePasswordAsync(string username, string newPassword);

        /// <summary>
        /// Upload user image and thumbnail
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        ServiceResult<string> UploadUserImage(FileUploadRequest file);
    }
}
