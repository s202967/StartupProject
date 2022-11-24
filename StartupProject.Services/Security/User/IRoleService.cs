using StartupProject.Core.Security.Identity;
using StartupProject.Core.ServiceResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StartupProject.Services.Security.User
{
    /// <summary>
    /// Role service abstraction
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Creates new role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<ServiceResult<ApplicationRole>> CreateAsync(ApplicationRole role);

        /// <summary>
        /// Delete the record
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<IServiceResult> DeleteAsync(string roleId);

        /// <summary>
        /// Gets role by role identifier
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<ServiceResult<ApplicationRole>> FindByIdAsync(string roleId);

        /// <summary>
        /// Find role by role name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        Task<ServiceResult<ApplicationRole>> FindByNameAsync(string roleName);

        /// <summary>
        /// Gets all roles
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<ApplicationRole>>> GetRolesAsync();

        /// <summary>
        /// Updates roles
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<ServiceResult<ApplicationRole>> UpdateAsync(ApplicationRole role);
    }
}
