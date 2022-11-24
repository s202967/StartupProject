using StartupProject.Client.Extension;
using StartupProject.Client.Features.Security.User.Dto;
using StartupProject.Client.Features.Security.User.Factories;
using StartupProject.Client.Filters;
using StartupProject.Core.Domain;
using StartupProject.Core.Security.Identity.User;
using StartupProject.Core.ServiceResult;
using StartupProject.Services.Security.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace StartupProject.Client.Features.Security.User.Controllers
{
    /// <summary>
    /// Contains user related service
    /// </summary>
    [Route("v1/security/")]
    public class UserController : BasePublicController
    {

        #region Ctor/Properties

        private const string AuthSchemes = JwtBearerDefaults.AuthenticationScheme;

        private readonly IUserService _userService;
        private readonly IUserDtoFactory _userDtoFactory;
        private readonly ILogger _logger;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userDtoFactory"></param>
        /// <param name="logger"></param>
        public UserController(IUserService userService, IUserDtoFactory userDtoFactory, ILogger<UserController> logger)
        {
            _userService = userService;
            _userDtoFactory = userDtoFactory;
            _logger = logger;
        }

        #endregion

        #region Methods

        /// <summary>	
        ///Get all user	
        /// </summary>	
        /// <returns></returns>	
        [Route("users/list")]
        [Authorize(AuthenticationSchemes = AuthSchemes)]
        [HttpPost]
        public async Task<ActionResult<UserListSearchResultDto>> GetUserListAsync(UserSearchDto searchDto)
        {
            var userListSearchResult = await _userDtoFactory.GetUserListAsync(searchDto).ConfigureAwait(false);
            return Ok(userListSearchResult);
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route("users")]
        [ServiceFilter(typeof(ValidateDtoAttribute))]
        [ServiceFilter(typeof(UnitOfWorkAttribute))]
        //[Authorize(AuthenticationSchemes = AuthSchemes)]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]CreateUserDto dto)
        {
            string userImageServerName = null, userImageThumbnailServerName = null;

            if (dto.UserImage != default)
            {
                var uploadUserImageResp = _userService.UploadUserImage(
                    new FileUploadRequest(dto.UserImage.ReadBytes(), dto.UserImage.ContentType, dto.UserImage.FileName));
                if (uploadUserImageResp.Status)
                    userImageServerName = uploadUserImageResp.Data;
                else
                    return JOk(uploadUserImageResp, dto);
            }

            if (dto.UserImageThumbnail != default)
            {
                var uploadUserImageThumbnailResp = _userService.UploadUserImage(
                    new FileUploadRequest(dto.UserImageThumbnail.ReadBytes(), dto.UserImageThumbnail.ContentType, dto.UserImageThumbnail.FileName));
                if (uploadUserImageThumbnailResp.Status)
                    userImageThumbnailServerName = uploadUserImageThumbnailResp.Data;
                else
                    return JOk(uploadUserImageThumbnailResp, dto);
            }

            var entity = dto.ToEntity();
            entity.Password = dto.Password;
            entity.UserImage = userImageServerName;
            entity.UserImageThumbnail = userImageThumbnailServerName;
            var serviceResp = await _userService.CreateAsync(entity);
            if (serviceResp.Status)
                dto.Id = entity.Id;

            return JOk(serviceResp, dto);
        }

        /// <summary>
        /// Update user information
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route("users")]
        [ServiceFilter(typeof(ValidateDtoAttribute))]
        [ServiceFilter(typeof(UnitOfWorkAttribute))]
        [Authorize(AuthenticationSchemes = AuthSchemes)]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm]UserDto dto)
        {
            var getUserResp = await _userService.GetUserByUserIdAsync(dto.Id).ConfigureAwait(false);
            if (!getUserResp.Status)
                return JOk(getUserResp);

            string userImageServerName = null, userImageThumbnailServerName = null;

            if (dto.UserImage != default)
            {
                var uploadUserImageResp = _userService.UploadUserImage(
                    new FileUploadRequest(dto.UserImage.ReadBytes(), dto.UserImage.ContentType, dto.UserImage.FileName));
                if (uploadUserImageResp.Status)
                    userImageServerName = uploadUserImageResp.Data;
                else
                    return JOk(uploadUserImageResp, dto);
            }

            if (dto.UserImageThumbnail != default)
            {
                var uploadUserImageThumbnailResp = _userService.UploadUserImage(
                    new FileUploadRequest(dto.UserImageThumbnail.ReadBytes(), dto.UserImageThumbnail.ContentType, dto.UserImageThumbnail.FileName));
                if (uploadUserImageThumbnailResp.Status)
                    userImageThumbnailServerName = uploadUserImageThumbnailResp.Data;
                else
                    return JOk(uploadUserImageThumbnailResp, dto);
            }

            var entity = dto.ToEntity(getUserResp.Data);
            entity.UserImage = userImageServerName;
            entity.UserImageThumbnail = userImageThumbnailServerName;
            var serviceResp = await _userService.UpdateAsync(entity).ConfigureAwait(false);
            return JOk(serviceResp, dto);
        }

        /// <summary>	
        /// Gets user detail by user name	
        /// </summary>	
        /// <returns>User dto</returns>	
        [Route("users/{username}")]
        [Authorize(AuthenticationSchemes = AuthSchemes)]
        [HttpGet]
        public async Task<ActionResult<UserWithRoleDto>> GetDetailByUserName(string username)
        {
            var getUserResp = await _userService.GetUserByUserNameAsync(username).ConfigureAwait(false);
            return JOk(getUserResp, getUserResp.Status ? getUserResp.Data.ToUserWithRoleDto() : null);
        }

        /// <summary>	
        /// Delete a user
        /// </summary>	
        /// <param name="userId"></param>	
        /// <returns></returns>	
        [Route("users/{userId}")]
        [Authorize(AuthenticationSchemes = AuthSchemes)]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string userId)
        {
            var serviceResp = await _userService.DeleteAsync(userId).ConfigureAwait(false);
            return JOk(serviceResp);
        }

        /// <summary>	
        /// Retrieves current logged-in user details	
        /// </summary>	
        /// <returns></returns>	
        [Route("users/details")]
        [Authorize(AuthenticationSchemes = AuthSchemes)]
        [HttpGet]
        public async Task<ActionResult<UserCore>> GetUserAsync()
        {
            var currentUser = await _userService.GetUserDetailsByUserNameAsync(HttpContext.User.Identity.Name).ConfigureAwait(false);
            return Ok(currentUser);
        }

        /// <summary>
        /// Enable|disable user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="status">true:enable, false:disable</param>
        /// <returns></returns>
        [Route("users/status")]
        [Authorize(AuthenticationSchemes = AuthSchemes)]
        [HttpPut]
        public async Task<IActionResult> UpdateUserStatusAsync(string userId, bool status)
        {
            var serviceResp = await _userDtoFactory.UpdateUserStatusAsync(userId, status);
            return Ok(serviceResp);
        }

        /// <summary>	
        /// Send user name to user email	
        /// </summary>	
        /// <returns></returns>	
        [Route("users/send-username-to-email")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> SendUserNameToEmailAsync(string email)
        {
            _logger.LogInformation("Sending username to {email} at {RequestTime}", email, DateTime.Now);
            var response = await _userService.SendUserNameToEmailAsync(email).ConfigureAwait(false);
            return JOk(response);
        }

        /// <summary>	
        ///Send password reset link to registered user email	
        /// </summary>	
        /// <param name="dto"></param>	
        /// <returns></returns>	
        [HttpPost]
        [AllowAnonymous]
        [ServiceFilter(typeof(ValidateDtoAttribute))]
        [Route("users/forgot-password")]
        public async Task<ActionResult<ServiceResult>> ForgotPasswordAsync(ForgotPasswordReqDto dto)
        {
            var serviceResult = await _userService.SendResetPswLinkInEmailAsync(dto.Email);
            return JOk(serviceResult);
        }

        /// <summary>	
        /// Reset password using reset password token	
        /// </summary>	
        /// <param name="dto"></param>	
        /// <returns></returns>	
        [HttpPost]
        [ServiceFilter(typeof(ValidateDtoAttribute))]
        [AllowAnonymous]
        [Route("users/reset-password")]
        public async Task<ActionResult<ServiceResult>> ResetPassword(ResetPasswordDto dto)
        {
            var serviceResult = await _userService.ResetPasswordAsync(dto.UserId, dto.Code, dto.ConfirmPassword);
            return JOk(serviceResult);
        }

        /// <summary>	
        /// Change password	
        /// </summary>	
        /// <returns></returns>	
        [HttpPost]
        [Authorize(AuthenticationSchemes = AuthSchemes)]
        [ServiceFilter(typeof(ValidateDtoAttribute))]
        [Route("users/change-password")]
        public async Task<ActionResult<ServiceResult>> ChangePasswordAsync(ChangePasswordDto dto)
        {
            var serviceResult = await _userService.ChangePasswordAsync(User.Identity.Name, dto.CurrentPassword, dto.NewPassword);
            return JOk(serviceResult);
        }

        /// <summary>	
        /// Change other user password from admin account	
        /// </summary>	
        /// <returns></returns>	
        [HttpPost]
        [Authorize(AuthenticationSchemes = AuthSchemes)]
        [ServiceFilter(typeof(ValidateDtoAttribute))]
        [Route("users/change-user-password")]
        public async Task<ActionResult<ServiceResult>> ChangeUserPasswordAsync(ChangeUserPasswordDto dto)
        {
            var serviceResult = await _userService.ChangePasswordAsync(dto.UserName, dto.NewPassword);
            return JOk(serviceResult);
        }

        #endregion

    }
}
