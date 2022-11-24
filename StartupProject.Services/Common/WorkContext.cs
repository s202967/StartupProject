using StartupProject.Core;
using StartupProject.Core.Security.Identity.User;
using StartupProject.Services.Security.User;
using Microsoft.AspNetCore.Http;
using System;

namespace StartupProject.Services.Common
{
    public class WorkContext : IWorkContext
    {
        private UserCore _curentUser;        
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly object _lock = new Object();

        public WorkContext
        (
            IUserService userService,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _userService = userService;
            this._httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets current user
        /// </summary>
        public UserCore CurrentUser
        {
            get
            {
                lock (_lock)
                {
                    if (_curentUser != null)
                        return _curentUser;

                    var currentUserName = _httpContextAccessor.HttpContext.User.Identity.Name;
                    if (currentUserName == null)
                        return _curentUser;

                    _curentUser = _userService.GetUserDetailsByUserNameAsync(currentUserName).ConfigureAwait(false).GetAwaiter().GetResult();
                }
                return _curentUser;
            }
        }        
    }
}
