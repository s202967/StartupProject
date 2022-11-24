using StartupProject.Core.Security.Identity;
using StartupProject.EntityFramework.Security.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace StartupProject.EntityFramework.Security.Identity.Extensions
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class IdentityExtensions
    {
        public static ApplicationIdentityResult ToApplicationIdentityResult(this IdentityResult identityResult)
        {
            return identityResult == null ? null : new ApplicationIdentityResult(identityResult.Errors.Select(x => x.Code + x.Description).ToList(), identityResult.Succeeded);
        }

        public static SignInStatus ToApplicationIdentityResult(this SignInResult identityResult)
        {
            if (identityResult.Succeeded)
                return SignInStatus.Success;
            else if (identityResult.IsLockedOut)
                return SignInStatus.LockedOut;
            else if (identityResult.RequiresTwoFactor)
                return SignInStatus.RequiresTwoFactorAuthentication;
            else
                return SignInStatus.Failure;
        }

        public static ApplicationIdentityUser ToApplicationUser(this AppUser appUser)
        {
            if (appUser == null)
            {
                return null;
            }
            var applicationUser = new ApplicationIdentityUser();
            return applicationUser.CopyAppUserProperties(appUser);
        }

        public static ApplicationIdentityUser CopyAppUserProperties(this ApplicationIdentityUser applicationIdentityUser, AppUser appUser)
        {
            if (appUser == null)
            {
                return null;
            }

            if (applicationIdentityUser == null)
            {
                return null;
            }

            applicationIdentityUser.Id = appUser.Id;
            applicationIdentityUser.Email = appUser.Email;
            applicationIdentityUser.EmailConfirmed = appUser.EmailConfirmed;
            applicationIdentityUser.SecurityStamp = appUser.SecurityStamp;
            applicationIdentityUser.PhoneNumber = appUser.PhoneNumber;
            applicationIdentityUser.PhoneNumberConfirmed = appUser.PhoneNumberConfirmed;
            applicationIdentityUser.TwoFactorEnabled = appUser.TwoFactorEnabled;
            applicationIdentityUser.LockoutEnabled = appUser.LockoutEnabled;
            applicationIdentityUser.AccessFailedCount = appUser.AccessFailedCount;
            applicationIdentityUser.UserName = appUser.UserName;

            applicationIdentityUser.IsInactive = appUser.IsInactive;
            applicationIdentityUser.FullName = appUser.FullName;
            applicationIdentityUser.IsVerified = appUser.IsVerified;
            applicationIdentityUser.MobileNumber = appUser.MobileNumber;
            applicationIdentityUser.Affiliation = appUser.Affiliation;
            applicationIdentityUser.Country = appUser.Country;

            applicationIdentityUser.UserImage = appUser.UserImage;
            applicationIdentityUser.UserImageThumbnail = appUser.UserImageThumbnail;
            applicationIdentityUser.CreatedBy = appUser.CreatedBy;
            applicationIdentityUser.CreatedOn = appUser.CreatedOn ?? DateTime.Now;

            return applicationIdentityUser;
        }

        public static AppUser ToAppUser(this ApplicationIdentityUser applicationIdentityUser)
        {
            if (applicationIdentityUser == null)
            {
                return null;
            }
            var appUser = new AppUser();
            return appUser.CopyApplicationIdentityUserProperties(applicationIdentityUser);
        }

        public static AppUser CopyApplicationIdentityUserProperties(this AppUser appUser, ApplicationIdentityUser applicationIdentityUser)
        {
            if (appUser == null)
            {
                return null;
            }

            if (applicationIdentityUser == null)
            {
                return null;
            }

            appUser.Id = applicationIdentityUser.Id;
            appUser.Email = applicationIdentityUser.Email;
            appUser.EmailConfirmed = applicationIdentityUser.EmailConfirmed;
            appUser.SecurityStamp = applicationIdentityUser.SecurityStamp;
            appUser.PhoneNumber = applicationIdentityUser.PhoneNumber;
            appUser.PhoneNumberConfirmed = applicationIdentityUser.PhoneNumberConfirmed;
            appUser.TwoFactorEnabled = applicationIdentityUser.TwoFactorEnabled;
            appUser.LockoutEnabled = applicationIdentityUser.LockoutEnabled;
            appUser.AccessFailedCount = applicationIdentityUser.AccessFailedCount;
            appUser.UserName = applicationIdentityUser.UserName;

            appUser.IsInactive = applicationIdentityUser.IsInactive;
            appUser.FullName = applicationIdentityUser.FullName;

            appUser.UserImage = applicationIdentityUser.UserImage;
            appUser.UserImageThumbnail = applicationIdentityUser.UserImageThumbnail;

            appUser.IsVerified = applicationIdentityUser.IsVerified;
            appUser.MobileNumber = applicationIdentityUser.MobileNumber;
            appUser.Affiliation = applicationIdentityUser.Affiliation;
            appUser.Country = applicationIdentityUser.Country;

            appUser.CreatedBy = applicationIdentityUser.CreatedBy;
            appUser.CreatedOn = applicationIdentityUser.CreatedOn;
            appUser.ModifiedBy = applicationIdentityUser.ModifiedBy;
            appUser.ModifiedOn = applicationIdentityUser.ModifiedOn;

            return appUser;
        }

        public static ApplicationUserRole ToApplicationUserRole(this IdentityUserRole<string> role)
        {
            return role == null ? null : new ApplicationUserRole
            {
                RoleId = role.RoleId,
                UserId = role.UserId
            };
        }

        public static ApplicationIdentityUserRole ToIdentityUserRole(this ApplicationUserRole role)
        {
            if (role == null) return null;

            var identityUserRole = new ApplicationIdentityUserRole
            {
                UserId = role.UserId,
                RoleId = role.RoleId
            };

            return identityUserRole;
        }

        public static ApplicationRole ToApplicationRole(this ApplicationIdentityRole identityRole)
        {
            if (identityRole == null) return null;
            var applicationRole = new ApplicationRole();
            return applicationRole.CopyIdentityRoleProperties(identityRole);
        }

        public static ApplicationRole CopyIdentityRoleProperties(this ApplicationRole applicationRole, ApplicationIdentityRole identityRole)
        {
            if (identityRole == null)
            {
                return null;
            }

            if (applicationRole == null)
            {
                return null;
            }

            applicationRole.Name = identityRole.Name;
            applicationRole.Id = identityRole.Id;
            //applicationRole.RoleId = identityRole.RoleId;
            return applicationRole;
        }

        public static ApplicationIdentityRole ToIdentityRole(this ApplicationRole applicationRole)
        {
            if (applicationRole == null)
            {
                return null;
            }

            var identityRole = new ApplicationIdentityRole();
            return identityRole.CopyApplicationRoleProperties(applicationRole);
        }

        public static ApplicationIdentityRole CopyApplicationRoleProperties(this ApplicationIdentityRole identityRole, ApplicationRole applicationRole)
        {
            if (identityRole == null)
            {
                return null;
            }

            if (applicationRole == null)
            {
                return null;
            }

            identityRole.Name = applicationRole.Name;
            identityRole.Id = applicationRole.Id;
            //   identityRole.RoleId = applicationRole.RoleId;
            return identityRole;
        }

        public static UserLoginInfo ToUserLoginInfo(this ApplicationUserLoginInfo loginInfo)
        {
            return loginInfo == null ? null : new UserLoginInfo(loginInfo.LoginProvider, loginInfo.ProviderKey, loginInfo.ProviderKey);
        }

        public static ApplicationUserLoginInfo ToApplicationUserLoginInfo(this UserLoginInfo loginInfo)
        {
            return loginInfo == null ? null : new ApplicationUserLoginInfo(loginInfo.LoginProvider, loginInfo.ProviderKey);
        }

        public static IList<UserLoginInfo> ToUserLoginInfoList(this IList<ApplicationUserLoginInfo> list)
        {
            return list.Select(u => u.ToUserLoginInfo()).ToList();
        }

        public static IList<ApplicationUserLoginInfo> ToApplicationUserLoginInfoList(this IList<UserLoginInfo> list)
        {
            return list.Select(u => u.ToApplicationUserLoginInfo()).ToList();
        }

        public static IEnumerable<ApplicationUserRole> ToApplicationUserRoleList(this IEnumerable<IdentityUserRole<string>> list)
        {
            return list.Select(u => u.ToApplicationUserRole()).ToList();
        }

        public static IEnumerable<IdentityUserRole<string>> ToIdentityUserRoleList(this IEnumerable<ApplicationUserRole> list)
        {
            return list.Select(u => u.ToIdentityUserRole()).ToList();
        }

        public static IEnumerable<ApplicationRole> ToApplicationRoleList(this IEnumerable<ApplicationIdentityRole> list)
        {
            return list.Select(u => u.ToApplicationRole()).ToList();
        }

        public static IEnumerable<ApplicationIdentityRole> ToIdentityRoleList(this IEnumerable<ApplicationRole> list)
        {
            return list.Select(u => u.ToIdentityRole()).ToList();
        }

        public static IEnumerable<ApplicationIdentityUser> ToApplicationUserList(this IEnumerable<AppUser> list)
        {
            return list.Select(u => u.ToApplicationUser()).ToList();
        }

        public static IEnumerable<AppUser> ToAppUserList(this IEnumerable<ApplicationIdentityUser> list)
        {
            return list.Select(u => u.ToAppUser()).ToList();
        }
    }
}
