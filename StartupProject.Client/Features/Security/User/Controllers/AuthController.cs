using StartupProject.Client.Features.Security.User.Dto;
using StartupProject.Client.Filters;
using StartupProject.Core.ServiceResult;
using StartupProject.Services.Security.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace StartupProject.Client.Features.Security.User.Controllers
{
    /// <summary>
    /// Auth services
    /// </summary>
    [Route("v1/auth/")]
    public class AuthController : BasePublicController
    {

        #region Ctor and properties

        private readonly IAuthService _authService;

        private const string AuthSchemes = JwtBearerDefaults.AuthenticationScheme;
        
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="authService"></param>
        public AuthController(IAuthService authService)
        {

            _authService = authService;
        }

        #endregion

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <returns></returns>
        [Route("signin")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignInAsync(SignInDto dto)
        {
            var serviceResp = await _authService.SignIn(dto.UserName, dto.Password, isPersistent: false);
            return Ok(serviceResp);
        }

        /// <summary>
        /// Refresh token
        /// </summary>
        /// <returns></returns>'
        [Route("refresh-token")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshTokenAsync(string token)
        {
            var serviceResp = await _authService.RefreshTokenAsync(refreshToken: token);
            return JOk(serviceResp);
        }

        /// <summary>
        /// Sign out
        /// </summary>
        /// <returns></returns>
        [Route("signout")]
        [HttpPost]
        //[AllowAnonymous]
        [Authorize(AuthenticationSchemes = AuthSchemes)]
        public async Task<IActionResult> SignOutAsync()
        {
            await _authService.SignOutAsync();
            return JOk(new ServiceResult(true));
        }

    }
}