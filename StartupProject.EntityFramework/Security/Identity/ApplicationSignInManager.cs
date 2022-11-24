using StartupProject.Core.Security.Identity;
using StartupProject.EntityFramework.Security.Identity.Extensions;
using StartupProject.EntityFramework.Security.Identity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace StartupProject.EntityFramework.Security.Identity
{
    public class ApplicationSignInManager : SignInManager<ApplicationIdentityUser>, IApplicationSignInManager
    {
        public ApplicationSignInManager
        (
            UserManager<ApplicationIdentityUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<ApplicationIdentityUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<ApplicationIdentityUser>> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<ApplicationIdentityUser> confirmation
        ) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }

        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="isPersistent"></param>
        /// <param name="lockoutOnFailure"></param>
        /// <returns></returns>
        public new async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            SignInResult identityResult = await base.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        /// <summary>
        ///  Sign out the current user out of the application.
        /// </summary>
        /// <returns></returns>
        public override Task SignOutAsync()
        {
            return base.SignOutAsync();
        }

    }
}
