using StartupProject.Core.Caching;
using StartupProject.Core.Domain;
using StartupProject.Core.Domain.Enum;
using StartupProject.Core.Domain.Interfaces;
using StartupProject.Core.Domain.SpEntity;
using StartupProject.Core.Infrastructure.Extensions;
using StartupProject.Core.Security.Identity;
using StartupProject.Core.Security.Identity.User;
using StartupProject.Core.Security.UserActivity;
using StartupProject.Core.ServiceResult;
using StartupProject.Services.Common.Email;
using StartupProject.Services.Common.Localization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StartupProject.Services.Security.User
{
    public class UserService : IUserService, IUserTokenService
    {

        #region Ctor/Properties

        private readonly IApplicationUserManager _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILocalizationService _localizationService;
        private readonly IEmailService _emailService;
        private readonly IRoleService _roleService;
        private readonly IStaticCacheManager _cache;
        private readonly IUserRepository _userRepository;
        private readonly IHostingEnvironment _env;
        private readonly IClientInfoProvider _clientInfoProvider;

        private const string ServiceName = "User";

        public UserService
        (
            IApplicationUserManager userManager,
            IHttpContextAccessor contextAccessor,
            IClientInfoProvider clientInfoProvider,
            ILocalizationService localizationService,
            IEmailService emailService,
            IRoleService roleService,
            IStaticCacheManager cache,
            IUserRepository userRepository,
            IHostingEnvironment env
        ) : base()
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _localizationService = localizationService;
            _emailService = emailService;
            _roleService = roleService;
            _cache = cache;
            _userRepository = userRepository;
            _env = env;
            _clientInfoProvider = clientInfoProvider;
        }

        #endregion

        #region Methods

        public async Task<IList<AppUser>> GetUsersAsync()
        {
            return await _cache.GetAsync(CacheKeys.UsersCacheKey, async () =>
            {
                var users = await _userManager.GetAllUsersAsync().ConfigureAwait(false);
                foreach (var item in users)
                {
                    if (item.Roles.Any())
                    {
                        foreach (var name in item.Roles)
                        {
                            var servicResp = await _roleService.FindByNameAsync(name).ConfigureAwait(false);
                            if (servicResp.Status)
                                item.URoles.Add(servicResp.Data);
                        }
                    }
                }
                return users.ToList();
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Ger user list
        /// </summary>
        /// <param name="userName">User name filter</param>
        /// <param name="status">True:active users, False:disabled users</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public async Task<IEnumerable<UserListProcResult>> GetUserListAsync(string userName, bool status, int pageIndex, int pageSize)
        {
            return await _userRepository.GetUserListAsync(userName, status, pageIndex, pageSize).ConfigureAwait(false);
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<ServiceResult<AppUser>> CreateAsync(AppUser entity)
        {
            entity.EmailConfirmed = true;
            entity.Id = Guid.NewGuid().ToString();
            entity.CreatedBy = GetCurrentUserId();
            var createUserResp = await _userManager.CreateAsync(entity).ConfigureAwait(false);
            _cache.Remove(CacheKeys.UsersCacheKey);

            if (createUserResp.Succeeded)
                return new ServiceResult<AppUser>(true, _localizationService.GetResource("AddNewRecord").FormatString(new string[] { ServiceName }).ToStringList());

            return new ServiceResult<AppUser>(false, createUserResp.Errors, MessageType.Danger);
        }

        /// <summary>
        /// Update user information
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<ServiceResult<AppUser>> UpdateAsync(AppUser entity)
        {
            entity.ModifiedBy = GetCurrentUserId();
            var updateUserResp = await _userManager.UpdateAsync(entity).ConfigureAwait(false);
            _cache.Remove(string.Format(CacheKeys.UserByUserNameCacheKey, entity.UserName));
            _cache.Remove(CacheKeys.UsersCacheKey);

            if (updateUserResp.Succeeded)
                return new ServiceResult<AppUser>(true, _localizationService.GetResource("EditRecord").FormatString(new string[] { ServiceName }).ToStringList());

            return new ServiceResult<AppUser>(false, updateUserResp.Errors, MessageType.Danger);
        }

        /// <summary>	
        /// Delete by user id	
        /// </summary>	
        /// <param name="userId"></param>	
        /// <returns></returns>	
        public async Task<ServiceResult> DeleteAsync(string userId)
        {
            var user = await _userManager.FindByUserIdAsync(userId).ConfigureAwait(false);
            if (user == null)
                return new ServiceResult(false, _localizationService.GetResource("RecordNotFound").ToStringList());

            var serviceResp = await _userManager.DeleteUserAsync(userId).ConfigureAwait(false);
            _cache.Remove(string.Format(CacheKeys.UserByUserNameCacheKey, user.UserName));
            _cache.Remove(CacheKeys.UsersCacheKey);

            if (serviceResp.Succeeded)
                return new ServiceResult(true, _localizationService.GetResource("DeleteRecord").FormatString(new string[] { ServiceName }).ToStringList());

            return new ServiceResult(false, serviceResp.Errors, MessageType.Danger);
        }

        /// <summary>	
        /// Find by username	
        /// </summary>	
        /// <param name="userName"></param>	
        /// <returns></returns>	
        public async Task<ServiceResult<AppUser>> GetUserByUserNameAsync(string userName)
        {
            return await _cache.GetAsync(string.Format(CacheKeys.UserByUserNameCacheKey, userName), async () =>
            {
                var user = await _userManager.FindByUserNameAsync(userName).ConfigureAwait(false);
                if (user == null)
                    return new ServiceResult<AppUser>(false, _localizationService.GetResource("RecordNotFound").FormatString(new string[] { ServiceName }).ToStringList(), MessageType.Warning);
                if (user.Roles.Any())
                {
                    foreach (var item in user.Roles)
                    {
                        var servicResp = await _roleService.FindByNameAsync(item).ConfigureAwait(false);
                        if (servicResp.Status)
                            user.URoles.Add(servicResp.Data);
                    }
                }
                return new ServiceResult<AppUser>(true)
                {
                    Data = user
                };
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Get user details by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ServiceResult<AppUser>> GetUserByUserIdAsync(string userId)
        {
            var user = await _userManager.FindByUserIdAsync(userId).ConfigureAwait(false);
            return new ServiceResult<AppUser>(true) { Data = user };
        }

        /// <summary>	
        /// Gets user details	
        /// </summary>	
        /// <param name="userName"></param>	
        /// <returns></returns>	
        public async Task<UserCore> GetUserDetailsByUserNameAsync(string userName)
        {
            var getUserResp = await GetUserByUserNameAsync(userName).ConfigureAwait(false);
            if (!getUserResp.Status)
                return new UserCore();

            var userEntity = getUserResp.Data;
            if (userEntity != null)
            {
                var curentUser = new UserCore
                {
                    UserId = new Guid(userEntity.Id),
                    Email = userEntity.Email,
                    PhoneNumber = userEntity.PhoneNumber,
                    UserName = userEntity.UserName,
                    FullName = userEntity.FullName,
                    UserImage = userEntity.UserImage,
                    UserImageThumbnail = userEntity.UserImageThumbnail,
                    RoleId = userEntity.URoles.FirstOrDefault()?.Id,
                    RoleName = userEntity.URoles.FirstOrDefault()?.Name,

                };
                return curentUser;
            }
            return new UserCore();
        }

        /// <summary>	
        /// Send user name to email	
        /// </summary>	
        /// <param name="senderEmail">sender's email</param>	
        /// <param name="email"> User email</param>	
        /// <returns></returns>	
        public async Task<ServiceResult> SendUserNameToEmailAsync(string email)
        {
            var userEntity = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);
            if (userEntity != default && !userEntity.IsInactive)
            {
                var mailContent = _emailService.GetEmailContentByType((int)EmailContentType.SendUserNameInMail);

                if (string.IsNullOrEmpty(mailContent?.Body))
                {
                    return new ServiceResult(false, _localizationService.GetResource("User.Email.NotExist").ToStringList(), MessageType.Warning);
                }

                string name = userEntity.FullName ?? userEntity.UserName;
                mailContent.Body = mailContent.Body.Replace("#Name#", name, StringComparison.OrdinalIgnoreCase)
                    .Replace("#UserName#", userEntity.UserName, StringComparison.OrdinalIgnoreCase);

                var response = await _emailService.GetSender.SendAsync(mailContent.Subject, userEntity.Email, mailContent.Body, "").ConfigureAwait(false);

                if (response.Status)
                    return new ServiceResult(true)
                    {
                        Message = _localizationService.GetResource("User.Name.SentSuccessfully").ToStringList()
                    };
                return response;
            }

            return new ServiceResult(false, _localizationService.GetResource("User.Email.NotExist").ToStringList(), MessageType.Warning);
        }

        /// <summary>	
        /// Send  password reset link to the respective email address	
        /// </summary>	
        /// <param name="senderEmail"></param>	
        /// <param name="email"></param>	
        /// <param name="iPAddress"></param>	
        /// <returns></returns>	
        public async Task<ServiceResult> SendResetPswLinkInEmailAsync(string email)
        {
            var callbackUrl = String.Format("<a href=\"{0}\">here</a>", _clientInfoProvider.Uri
               .Replace("v1/security/users/forgot-password", "reset-password", StringComparison.OrdinalIgnoreCase) + "?code=" + "{token}" + "&userId=" + "{userId}");

            var userEntity = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);

            if (userEntity != default && !userEntity.IsInactive)
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(userEntity.Id.ToString()).ConfigureAwait(false);
                callbackUrl = callbackUrl.Replace("{token}", code, StringComparison.OrdinalIgnoreCase).Replace("{userId}", userEntity.Id.ToString(), StringComparison.OrdinalIgnoreCase);

                var mailContent = _emailService.GetEmailContentByType((int)EmailContentType.SendResetPasswordLinkInMail);

                if (string.IsNullOrEmpty(mailContent?.Body))
                {
                    return new ServiceResult(false, _localizationService.GetResource("User.Email.NotExist").ToStringList(), MessageType.Danger);
                }

                string name = userEntity.FullName ?? userEntity.UserName;
                var body = mailContent.Body.Replace("#Name#", name, StringComparison.OrdinalIgnoreCase)
                    .Replace("#Link#", callbackUrl, StringComparison.OrdinalIgnoreCase);

                var response = await _emailService.GetSender.SendAsync(mailContent.Subject, userEntity.Email, body).ConfigureAwait(false);


                return response;
            }

            return new ServiceResult(false, _localizationService.GetResource("User.Email.NotExist").ToStringList(), MessageType.Danger);
        }

        /// <summary>	
        /// Reset password using password reset token	
        /// </summary>	
        /// <param name="userId">User Id</param>	
        /// <param name="code">Token</param>	
        /// <param name="password"> Password</param>	
        /// <returns>Service result</returns>	
        public async Task<ServiceResult> ResetPasswordAsync(string userId, string code, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(userId, code, newPassword).ConfigureAwait(false);
            if (result.Succeeded)
            {
                return new ServiceResult(true, _localizationService.GetResource("User.Password.Updated").ToStringList());
            }
            if (!result.Errors.Any())
            {
                return new ServiceResult(false, _localizationService.GetResource("User.Password.ChangeFailed").ToStringList(), MessageType.Danger);
            }
            return new ServiceResult(false, result.Errors.ToList(), MessageType.Danger.ToString());
        }

        /// <summary>	
        /// Change password	
        /// </summary>	
        /// <param name="username"></param>	
        /// <param name="currentPassword"></param>	
        /// <param name="newPassword"></param>	
        /// <returns></returns>	
        public async Task<ServiceResult> ChangePasswordAsync(string username, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByUserNameAsync(username).ConfigureAwait(false);
            var resp = await _userManager.ChangePasswordAsync(user.Id.ToString(), currentPassword, newPassword).ConfigureAwait(false);
            if (resp.Succeeded)
                return new ServiceResult(resp.Succeeded, _localizationService.GetResource("User.Password.Changed").ToStringList());
            return new ServiceResult(resp.Succeeded, resp.Errors.ToList(), MessageType.Danger);
        }

        /// <summary>	
        /// Change other user password from admin account	
        /// </summary>	
        /// <param name="username"></param>	
        /// <param name="newPassword"></param>	
        /// <returns></returns>	
        public async Task<ServiceResult> ChangePasswordAsync(string username, string newPassword)
        {
            var user = await _userManager.FindByUserNameAsync(username).ConfigureAwait(false);
            if (user == null)
                return new ServiceResult<AppUser>(false, _localizationService.GetResource("RecordNotFound").FormatString(new string[] { ServiceName }).ToStringList(), MessageType.Warning);
            var serviceRes = await _userManager.ResetPasswordAsync(user.Id.ToString(), newPassword).ConfigureAwait(false);
            if (serviceRes.Succeeded)
                return new ServiceResult(true, _localizationService.GetResource("User.Password.Changed").ToStringList());
            return new ServiceResult(false, serviceRes.Errors.Any() ? serviceRes.Errors : _localizationService.GetResource("User.Password.ChangeFailed").ToStringList(), MessageType.Danger);
        }

        /// <summary>	
        /// Get current logged-in user id from claim	
        /// </summary>	
        /// <returns></returns>	
        private string GetCurrentUserId()
        {
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var claim = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                return claim?.Value;
            }
            return string.Empty;
        }

        #endregion

        #region User Token

        /// <summary>
        /// Generate token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="purpose"></param>
        /// <returns></returns>
        public async Task<string> GeneratAsync(string username, string purpose)
        {
            return await _userManager.GenerateUserTokenAsync(username, purpose).ConfigureAwait(false);
        }

        /// <summary>
        /// Verify user token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="purpose"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> VerifyAsync(string username, string purpose, string token)
        {
            return await _userManager.VerifyUserTokenAsync(username, purpose, token).ConfigureAwait(false);
        }

        #endregion

        #region User Image

        /// <summary>
        /// Upload user image and thumbnail
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public ServiceResult<string> UploadUserImage(FileUploadRequest file)
        {
            try
            {
                //if user image is empty 
                if (file == default)
                    return new ServiceResult<string>(true, _localizationService.GetResource("Attachement.NotFound").ToStringList(), MessageType.Info);

                //upload location
                string uploadLocation = "Uploads/";
                var fileDir = Path.Combine(_env.WebRootPath, uploadLocation);

                //if directory not exists, then create it
                if (!Directory.Exists(fileDir))
                    Directory.CreateDirectory(fileDir);

                string userFileName = file.FileName;
                string ext = Path.GetExtension(userFileName);

                //only valid image format will be allowed to upload
                if (!IsUserImageValidForUpload(ext))
                    return new ServiceResult<string>(false, _localizationService.GetResource("Attachement.Invalid").FormatString(new string[] { ext }).ToStringList(), MessageType.ValidationFailed);

                //TODO:: validate image size
                double fileSize = file.File.Length;

                string serverFileName = Guid.NewGuid().ToString() + ext;
                string fullPath = Path.Combine(fileDir, serverFileName);

                if (UploadFile(file.File, fullPath))
                {
                    return new ServiceResult<string>(true)
                    {
                        Message = _localizationService.GetResource("Attachement.Uploaded").FormatString(new string[] { userFileName }).ToStringList(),
                        MessageType = MessageType.Success,
                        Data = serverFileName
                    };
                }
                else
                {
                    return new ServiceResult<string>(false)
                    {
                        Message = _localizationService.GetResource("Attachement.Error").FormatString(new string[] { userFileName }).ToStringList(),
                        MessageType = MessageType.Danger
                    };
                }
            }
            catch (Exception)
            {
                return new ServiceResult<string>(false, _localizationService.GetResource("Attachement.Failed").ToStringList(), MessageType.Danger);
            }
        }

        protected bool IsUserImageValidForUpload(string fileExtension)
        {
            if (string.IsNullOrEmpty(fileExtension))
                return true;

            if (fileExtension.ToLower() == ".png" || fileExtension.ToLower() == ".jpeg" ||
                fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".bmp" ||
                fileExtension.ToLower() == ".gif")
            {
                return true;
            }
            return false;
        }

        protected bool UploadFile(byte[] file, string path)
        {
            try
            {
                File.WriteAllBytes(path, file);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

    }
}
