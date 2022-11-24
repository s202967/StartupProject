using StartupProject.Core.Infrastructure.Cryptography;
using StartupProject.Core.Infrastructure.Extensions;
using StartupProject.Core.Infrastructure.Helper;
using StartupProject.Core.Security.Identity;
using StartupProject.Core.Security.UserActivity;
using StartupProject.Core.ServiceResult;
using StartupProject.Services.Common.Localization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StartupProject.Services.Security.User
{
    /// <summary>
    /// Auth services
    /// </summary>
    public class AuthService : IAuthService
    {
        #region Ctor and properties

        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationSignInManager _signInManager;
        private readonly IdentityConfig _identityConfig;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private readonly ILocalizationService _localizationService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IClientInfoProvider _clientInfoProvider;
        // private readonly IWebHostEnvironment _hostingEnviroment;
        public AuthService
        (
            IApplicationUserManager userManager,
            IApplicationSignInManager signInManager,
            IHttpContextAccessor contextAccessor,
            IClientInfoProvider clientInfoProvider,
            IOptions<IdentityConfig> identityConfig,
            ILocalizationService localizationService,
            IRefreshTokenService refreshTokenService,
            IConfiguration configuration
        // ,IWebHostEnvironment hostingEnviroment

        ) : base()
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _identityConfig = identityConfig.Value;
            _contextAccessor = contextAccessor;
            _localizationService = localizationService;
            _refreshTokenService = refreshTokenService;
            _clientInfoProvider = clientInfoProvider;

            //  _hostingEnviroment = hostingEnviroment;

        }

        #endregion

        #region Sign In/Sign out

        /// <summary>
        /// Sign In user
        /// </summary>
        /// <param name="username">User name</param>
        /// <param name="password">Password</param>
        /// <param name="isPersistent"></param>
        /// <returns></returns>
        public async Task<ServiceResult<SignInResponse>> SignIn(string username, string password, bool isPersistent)
        {
            var user = await _userManager.FindByUserNameAsync(username).ConfigureAwait(false);
            if (user == null || user.IsInactive)
            {
                return new ServiceResult<SignInResponse>(false, _localizationService.GetResource("User.Credential.Invalid").ToStringList(), MessageType.Warning);
            }

            if (!user.IsVerified)
                return new ServiceResult<SignInResponse>(false, "User is not verified.".ToStringList(), MessageType.Warning);

            return await PasswordSignInAsync(user, password, isPersistent).ConfigureAwait(false);
        }

        /// <summary>
        /// Refresh token
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<SignInResponse>> RefreshTokenAsync(string refreshToken)
        {
            var storedToken = _refreshTokenService.GetByToken(refreshToken, _clientInfoProvider.ClientIpAddress);
            if (storedToken == null || !storedToken.Status)
            {
                return new ServiceResult<SignInResponse>(false, _localizationService.GetResource("User.RefreshToken.Invalid").ToStringList(), MessageType.Warning);
            }
            if ((storedToken.Data.CreatedOn - DateTimeFormatter.CurrentDateTimeUtc).TotalMinutes > _identityConfig.ApiToken.RefreshExpireMinutes)
            {
                return new ServiceResult<SignInResponse>(false, _localizationService.GetResource("User.RefreshToken.Invalid").ToStringList(), MessageType.Warning);
            }
            var user = await _userManager.FindByUserNameAsync(storedToken.Data.UserName).ConfigureAwait(false);
            var (token, expires) = GetToken(user.UserName, user.Id);
            storedToken.Data.Token = GenerateRefreshToken();
            storedToken.Data.ObfuscatedToken = token.Encrypt(AesKeys.RefreshTokenAesKey);
            await _refreshTokenService.UpdateAsync(storedToken.Data).ConfigureAwait(false);
            var resp = new ServiceResult<SignInResponse>(true)
            {
                Data = new SignInResponse
                {
                    //Token based login
                    Token = token,
                    RefreshToken = storedToken.Data.Token,
                    Username = user.UserName,
                    ExpireMinutes = expires,
                    IdleTimeoutMinutes = _identityConfig.Custom.UserIdleTimeoutMinutes
                },
                Message = _localizationService.GetResource("User.Token.Refresh").ToStringList()
            };
            return resp;
        }

        /// <summary>
        /// Sign out
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<ServiceResult> SignOutAsync()
        {
            var username = _contextAccessor.HttpContext.User.Identity.IsAuthenticated ? _contextAccessor.HttpContext.User.Identity.Name : string.Empty;
            var serviceResult = new ServiceResult(true);
            if (!string.IsNullOrEmpty(username))
                serviceResult = await _refreshTokenService.DeleteAsync(username, _clientInfoProvider.ClientIpAddress).ConfigureAwait(false);
            await _signInManager.SignOutAsync().ConfigureAwait(false);
            await _contextAccessor.HttpContext.SignOutAsync().ConfigureAwait(false);
            return serviceResult;
        }

        /// <summary>
        /// Authenticate with password
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <param name="isMobileUser"></param>
        /// <param name="isPersistent"></param>
        /// <returns></returns>
        private async Task<ServiceResult<SignInResponse>> PasswordSignInAsync(AppUser user, string password, bool isPersistent)
        {
            var signInStatus = await _signInManager.PasswordSignInAsync(user.UserName, password, isPersistent, true).ConfigureAwait(false);

            if (signInStatus == SignInStatus.LockedOut)
            {
                //get lockout end date 
                var lockoutEnd = await _userManager.GetLockEndLocalDateTimeAsync(user.UserName).ConfigureAwait(false);
                var timeDiff = lockoutEnd.Subtract(DateTime.Now);
                return new ServiceResult<SignInResponse>(true, new List<string> { string.Format(_localizationService.GetResource("User.locked"), $"{ (int)timeDiff.TotalHours }:{timeDiff.Minutes}") });
            }

            // If sign is successful, generate jwt token
            if (signInStatus == SignInStatus.Success)
            {
                //prevent multiple login for same user
                _ = bool.TryParse(_configuration["IdentityOptions:PreventMultipleLoginForSameUser"], out bool preventMultipleLoginForSameUser);
                if (preventMultipleLoginForSameUser)
                {
                    await _userManager.UpdateSecurityStampAsync(user.Id.ToString()).ConfigureAwait(false);
                }

                return await GenerateResourceToken(user.UserName, user.Id).ConfigureAwait(false);
            }
            return new ServiceResult<SignInResponse>(false, _localizationService.GetResource("User.Credential.Invalid").ToStringList());
        }

        /// <summary>
        /// Generate API token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<ServiceResult<SignInResponse>> GenerateResourceToken(string username, string userId)
        {
            var (token, expires) = GetToken(username, userId);
            var refreshToken = _refreshTokenService.GetByUserName(username, _clientInfoProvider.ClientIpAddress);
            if (refreshToken != null)
            {
                refreshToken.Token = GenerateRefreshToken();
                refreshToken.ClientIpAddress = _clientInfoProvider.ClientIpAddress;
                refreshToken.ObfuscatedToken = token.Encrypt(AesKeys.RefreshTokenAesKey);
                await _refreshTokenService.UpdateAsync(refreshToken).ConfigureAwait(false);
            }
            else
            {
                refreshToken = new RefreshToken(username, GenerateRefreshToken(), string.Empty, _clientInfoProvider.ClientIpAddress)
                {
                    ObfuscatedToken = token.Encrypt(AesKeys.RefreshTokenAesKey)
                };
                await _refreshTokenService.CreateAsync(refreshToken).ConfigureAwait(false);
            }

            var resp = new ServiceResult<SignInResponse>(true)
            {
                Data = new SignInResponse
                {
                    //Token based login
                    Token = token,
                    RefreshToken = refreshToken.Token,
                    Username = username,
                    ExpireMinutes = expires,
                    IdleTimeoutMinutes = _identityConfig.Custom.UserIdleTimeoutMinutes
                },
                Message = _localizationService.GetResource("User.Authentication.Succeed").ToStringList()
            };


            return resp;
        }

        /// <summary>
        /// Creates JWT token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private (string token, int expires) GetToken(string username, string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_identityConfig.ApiToken.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.NameIdentifier, userId)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_identityConfig.ApiToken.AccessExpireMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            //UserIdleTimeoutMinutes
            var token = tokenHandler.CreateToken(tokenDescriptor);
            tokenHandler.WriteToken(token);

            return (tokenHandler.WriteToken(token), _identityConfig.ApiToken.AccessExpireMinutes);
        }

        /// <summary>
        /// Creates user identity
        /// </summary>
        /// <param name="user"></param>
        /// <param name="authenticationType"></param>
        /// <returns></returns>
        private static ClaimsIdentity CreateIdentity(AppUser user, string authenticationType)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };
            var claimsIdentity = new ClaimsIdentity(claims, authenticationType);
            return claimsIdentity;
        }

        /// <summary>
        /// Generates reset token
        /// </summary>
        /// <returns></returns>
        private static string GenerateRefreshToken()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        #endregion
    }
}
