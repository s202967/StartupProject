using StartupProject.Core.Security.Identity;
using StartupProject.EntityFramework.Security.Identity.Extensions;
using StartupProject.EntityFramework.Security.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace StartupProject.EntityFramework.Security.Identity
{
    /// <summary>
    /// Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    /// </summary>
    public class ApplicationUserManager : UserManager<ApplicationIdentityUser>, IApplicationUserManager
    {
        private const string tokenProvider = "StartupProject";

        public ApplicationUserManager
        (IUserStore<ApplicationIdentityUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApplicationIdentityUser> passwordHasher,
            IEnumerable<IUserValidator<ApplicationIdentityUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationIdentityUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<ApplicationIdentityUser>> logger
        ) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public async Task<AppUser> FindByUserNameAsync(string userName)
        {
            var user = await base.FindByNameAsync(userName).ConfigureAwait(false);
            if (user == null)
                return null;

            var appUser = user.ToAppUser();
            
            appUser.Roles = await base.GetRolesAsync(user).ConfigureAwait(false);
            return appUser;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string userId)
        {
            var user = await base.FindByIdAsync(userId).ConfigureAwait(false);
            return await base.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual async Task<ApplicationIdentityResult> CreateAsync(AppUser user)
        {
            var applicationUser = user.ToApplicationUser();

            _ = new IdentityResult();
            IdentityResult identityResult;

            if (string.IsNullOrEmpty(user.Password))
                identityResult = await base.CreateAsync(applicationUser).ConfigureAwait(false);
            else
                identityResult = await base.CreateAsync(applicationUser, user.Password).ConfigureAwait(false);

            if (identityResult.Succeeded && user.Roles.Any())
            {
                await base.AddToRolesAsync(applicationUser, user.Roles).ConfigureAwait(false);
            }

            user.CopyApplicationIdentityUserProperties(applicationUser);
            return identityResult.ToApplicationIdentityResult();
        }

        /// <summary>
        /// Generate authentication token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="purpose"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string> GenerateUserTokenAsync(string username, string purpose)
        {
            var userEntity = await base.FindByNameAsync(username).ConfigureAwait(false);
            return await base.GenerateUserTokenAsync(userEntity, tokenProvider, purpose).ConfigureAwait(false);
        }

        /// <summary>
        /// Validate user token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="purpose"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual async Task<bool> VerifyUserTokenAsync(string username, string purpose, string token)
        {
            var userEntity = await base.FindByNameAsync(username).ConfigureAwait(false);
            return await base.VerifyUserTokenAsync(userEntity, tokenProvider, purpose, token).ConfigureAwait(false);
        }

        public virtual async Task<ApplicationIdentityResult> DeleteAsync(string userId)
        {
            var applicationUser = await base.FindByIdAsync(userId).ConfigureAwait(false);
            if (applicationUser == null)
            {
                return new ApplicationIdentityResult(new List<string> { "Invalid user Id" }, false);
            }
            var identityResult = await base.DeleteAsync(applicationUser).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public new virtual async Task<AppUser> FindByNameAsync(string userName)
        {
            var user = await base.FindByNameAsync(userName).ConfigureAwait(false);
            return user.ToAppUser();
        }

        public new virtual async Task<AppUser> FindByEmailAsync(string email)
        {
            var user = await base.FindByEmailAsync(email).ConfigureAwait(false);
            return user.ToAppUser();
        }

        public async Task<DateTime> GetLockEndLocalDateTimeAsync(string username)
        {
            var user = await base.FindByNameAsync(username).ConfigureAwait(false);
            return user.LockoutEnd.HasValue ? user.LockoutEnd.Value.LocalDateTime : DateTime.MaxValue;
        }

        public async Task<ApplicationIdentityResult> ResetPasswordAsync(string userId, string token, string newPassword)
        {
            var user = await base.FindByIdAsync(userId).ConfigureAwait(false);
            var identityResult = await base.ResetPasswordAsync(user, token, newPassword).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        /// <summary>	
        /// Change password	
        /// </summary>	
        /// <param name="userId"></param>	
        /// <param name="currentPassword"></param>	
        /// <param name="newPassword"></param>	
        /// <returns></returns>	
        public async Task<ApplicationIdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await base.FindByIdAsync(userId).ConfigureAwait(false);
            var resp = await base.ChangePasswordAsync(user, currentPassword, newPassword).ConfigureAwait(false);
            return resp.ToApplicationIdentityResult();
        }

        public async Task<ApplicationIdentityResult> ResetPasswordAsync(string userId, string newPassword)
        {
            var user = await base.FindByIdAsync(userId).ConfigureAwait(false);
            var removePswResp = await base.RemovePasswordAsync(user).ConfigureAwait(false);
            if (removePswResp.Succeeded)
            {
                var result = await base.AddPasswordAsync(user, newPassword).ConfigureAwait(false);
                return new ApplicationIdentityResult(result.Errors.Select(x => x.Description).ToList(), result.Succeeded);
            }
            return new ApplicationIdentityResult(new List<string>(), false);
        }

        /// <summary>
        /// Update existing user details
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        public virtual async Task<ApplicationIdentityResult> UpdateAsync(AppUser appUser)
        {
            ApplicationIdentityUser applicationIdentityUser = await base.FindByIdAsync(userId: appUser.Id.ToString(CultureInfo.CurrentCulture)).ConfigureAwait(false);

            applicationIdentityUser.Email = appUser.Email;

            if (!string.IsNullOrEmpty(appUser.Email))
                applicationIdentityUser.NormalizedEmail = appUser.Email.ToUpper();

            applicationIdentityUser.UserName = appUser.UserName;
            if (!string.IsNullOrEmpty(appUser.UserName))
                applicationIdentityUser.NormalizedUserName = appUser.UserName.ToUpper();

            applicationIdentityUser.AccessFailedCount = appUser.AccessFailedCount;
            applicationIdentityUser.EmailConfirmed = appUser.EmailConfirmed;
            applicationIdentityUser.PhoneNumber = appUser.PhoneNumber;
            applicationIdentityUser.PhoneNumberConfirmed = appUser.PhoneNumberConfirmed;
            applicationIdentityUser.SecurityStamp = appUser.SecurityStamp;
            applicationIdentityUser.TwoFactorEnabled = appUser.TwoFactorEnabled;
            applicationIdentityUser.IsInactive = appUser.IsInactive;
            applicationIdentityUser.FullName = appUser.FullName;
            
            applicationIdentityUser.UserImage = appUser.UserImage;
            applicationIdentityUser.UserImageThumbnail = appUser.UserImageThumbnail;
            applicationIdentityUser.ModifiedBy = appUser.ModifiedBy;
            applicationIdentityUser.ModifiedOn = appUser.ModifiedOn ?? DateTime.Now;

            var identityResult = await base.UpdateAsync(applicationIdentityUser).ConfigureAwait(false);

            if (!identityResult.Succeeded || !appUser.Roles.Any()) return identityResult.ToApplicationIdentityResult();

            var userRoles = await base.GetRolesAsync(applicationIdentityUser).ConfigureAwait(false);
            await base.RemoveFromRolesAsync(applicationIdentityUser, userRoles).ConfigureAwait(false);
            await base.AddToRolesAsync(applicationIdentityUser, appUser.Roles).ConfigureAwait(false);

            return identityResult.ToApplicationIdentityResult();
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<AppUser>> GetAllUsersAsync()
        {
            var userList = new List<AppUser>();
            var users = await base.Users.ToListAsync().ConfigureAwait(false);
            foreach (var item in users)
            {
                var appUser = item.ToAppUser();
                appUser.Roles = await base.GetRolesAsync(item).ConfigureAwait(false);
                userList.Add(appUser);
            }
            return userList;
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ApplicationIdentityResult> DeleteUserAsync(string userId)
        {
            var user = await base.FindByIdAsync(userId).ConfigureAwait(false);
            var userRoles = await base.GetRolesAsync(user).ConfigureAwait(false);

            await base.RemoveFromRolesAsync(user, userRoles).ConfigureAwait(false);
            var deleteUserResp = await base.DeleteAsync(user).ConfigureAwait(false);

            return deleteUserResp.ToApplicationIdentityResult();
        }

        /// <summary>
        /// update security stamp
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ApplicationIdentityResult> UpdateSecurityStampAsync(string userId)
        {
            var user = await base.FindByIdAsync(userId).ConfigureAwait(false);
            var resp = await base.UpdateSecurityStampAsync(user).ConfigureAwait(false);
            return resp.ToApplicationIdentityResult();
        }

        public async Task<AppUser> FindByUserIdAsync(string userId)
        {
            var user = await base.FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
                return null;

            var appUser = user.ToAppUser();
            appUser.Roles = await base.GetRolesAsync(user).ConfigureAwait(false);
            return appUser;
        }
    }
}
