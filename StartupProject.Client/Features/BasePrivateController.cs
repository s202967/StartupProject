using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace StartupProject.Client.Features
{
    /// <summary>
    /// This class will be inherited by all secured controllers.
    /// </summary>
    [Authorize(AuthenticationSchemes = AuthSchemes)]
    public class BasePrivateController : BaseController
    {

        private const string AuthSchemes = JwtBearerDefaults.AuthenticationScheme;

        /// <summary>
        /// Inject Logger service
        /// </summary>
        public BasePrivateController()
        {
        }
    }
}