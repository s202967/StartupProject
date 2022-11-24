using System.Collections.Generic;
using System.Threading.Tasks;

namespace StartupProject.Core.Security.Identity
{
    public interface IApplicationRoleManager
    {
        Task<ApplicationIdentityResult> CreateAsync(ApplicationRole role);
        Task<ApplicationIdentityResult> DeleteAsync(string roleId);
        Task<ApplicationRole> FindByIdAsync(string roleId);
        Task<ApplicationRole> FindByNameAsync(string roleName);
        Task<IEnumerable<ApplicationRole>> GetRolesAsync();
        Task<ApplicationIdentityResult> UpdateAsync(ApplicationRole applicationRole);
    }
}