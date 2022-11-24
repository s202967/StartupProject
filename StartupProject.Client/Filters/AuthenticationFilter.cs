using StartupProject.Core.Infrastructure.DependencyManagement;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StartupProject.Client.Filters
{
    /// <summary>
    /// Authentication filter
    /// </summary>
    public class AuthenticationFilter : IAsyncActionFilter
    {
        private readonly ISessionProvider _sessionProvider;

        /// <summary>
        /// Service injection
        /// </summary>
        /// <param name="sessionProvider"></param>
        public AuthenticationFilter(ISessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        /// <summary>
        /// Share user id to db context
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = context.HttpContext.User;           
            if (user.Identity.IsAuthenticated)
            {
                var claim = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (claim != null)
                    _sessionProvider.SetUserId(new Guid(claim.Value));
            }
            await next();
        }
    }
}
