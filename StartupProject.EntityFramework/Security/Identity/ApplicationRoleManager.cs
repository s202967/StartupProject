using StartupProject.Core.Security.Identity;
using StartupProject.EntityFramework.Security.Identity.Extensions;
using StartupProject.EntityFramework.Security.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StartupProject.EntityFramework.Security.Identity
{
    /// <summary>
    ///  Configure the RoleManager used in the application. RoleManager is defined in the ASP.NET Identity core assembly.
    /// </summary>
    public class ApplicationRoleManager : RoleManager<ApplicationIdentityRole>, IApplicationRoleManager
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="store"></param>
        /// <param name="roleValidators"></param>
        /// <param name="keyNormalizer"></param>
        /// <param name="errors"></param>
        /// <param name="logger"></param>
        public ApplicationRoleManager
        (
            IRoleStore<ApplicationIdentityRole> store,
            IEnumerable<IRoleValidator<ApplicationIdentityRole>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<RoleManager<ApplicationIdentityRole>> logger
        ) : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }

        /// <summary>
        /// Create new role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public virtual async Task<ApplicationIdentityResult> CreateAsync(ApplicationRole role)
        {
            var identityRole = role.ToIdentityRole();
            var identityResult = await base.CreateAsync(identityRole).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        /// <summary>
        /// Delete by Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public virtual async Task<ApplicationIdentityResult> DeleteAsync(string roleId)
        {
            var identityRole = await base.FindByIdAsync(roleId).ConfigureAwait(false);
            if (identityRole == null)
            {
                return null;
            }

            var identityResult = await base.DeleteAsync(identityRole).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        /// <summary>
        /// Find by Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public new virtual async Task<ApplicationRole> FindByIdAsync(string roleId)
        {
            var role = await base.FindByIdAsync(roleId).ConfigureAwait(false);
            return role.ToApplicationRole();
        }

        /// <summary>
        /// Find by role name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public new virtual async Task<ApplicationRole> FindByNameAsync(string roleName)
        {
            var role = await base.FindByNameAsync(roleName).ConfigureAwait(false);
            return role.ToApplicationRole();
        }

        /// <summary>
        /// Gets all roles
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<ApplicationRole>> GetRolesAsync()
        {
            var applicationRoles = await base.Roles.ToListAsync().ConfigureAwait(false);
            return applicationRoles.ToApplicationRoleList();
        }

        /// <summary>
        /// Update role name only
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public virtual async Task<ApplicationIdentityResult> UpdateAsync(string roleId, string roleName)
        {
            var identityRole = await base.FindByIdAsync(roleId).ConfigureAwait(false);
            if (identityRole == null)
            {
                return new ApplicationIdentityResult(new List<string> { "Invalid role Id" }, false);
            }
            identityRole.Name = roleName;
            var identityResult = await base.UpdateAsync(identityRole).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        /// <summary>
        /// update role with multiple fields
        /// </summary>
        /// <param name="applicationRole"></param>
        /// <returns></returns>
        public virtual async Task<ApplicationIdentityResult> UpdateAsync(ApplicationRole applicationRole)
        {
            var identityRole = await base.FindByIdAsync(applicationRole.Id).ConfigureAwait(false);
            if (identityRole == null)
            {
                return new ApplicationIdentityResult(new List<string> { "Invalid role Id" }, false);
            }

            identityRole.Name = applicationRole.Name;
           // identityRole.RoleId = applicationRole.RoleId;
            var identityResult = await base.UpdateAsync(identityRole).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

    }
}
