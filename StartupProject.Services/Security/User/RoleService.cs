using StartupProject.Core.Caching;
using StartupProject.Core.Infrastructure.Extensions;
using StartupProject.Core.Security.Identity;
using StartupProject.Core.ServiceResult;
using StartupProject.Services.Common.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StartupProject.Services.Security.User
{
    /// <summary>
    /// Role services
    /// </summary>
    public class RoleService : IRoleService
    {

        #region Ctor and properties

        private readonly IApplicationRoleManager _roleManager;
        private readonly ILocalizationService _localizationService;
        private readonly IStaticCacheManager _cache;

        private const string ServiceName = "Role";

        public RoleService
        (
            IApplicationRoleManager roleManager,
            ILocalizationService localizationService,
            IStaticCacheManager cache
        )
        {
            _roleManager = roleManager;
            _localizationService = localizationService;
            _cache = cache;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<ApplicationRole>>> GetRolesAsync()
        {
            return await _cache.GetAsync(CacheKeys.RolesCacheKey, async () =>
            {
                var roles = await _roleManager.GetRolesAsync().ConfigureAwait(false);
                return new ServiceResult<IEnumerable<ApplicationRole>>(true) { Data = roles };
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Find by role name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<ServiceResult<ApplicationRole>> FindByNameAsync(string roleName)
        {
            return await _cache.GetAsync(string.Format(CacheKeys.RoleByNameCacheKey, roleName), async () =>
            {
                var role = await _roleManager.FindByNameAsync(roleName).ConfigureAwait(false);
                if (role == null)
                    return new ServiceResult<ApplicationRole>(false, _localizationService.GetResource("RecordNotFound").FormatString(new string[] { ServiceName }).ToStringList(), MessageType.Warning);

                return new ServiceResult<ApplicationRole>(true)
                {
                    Data = role
                };
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Find by role id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<ServiceResult<ApplicationRole>> FindByIdAsync(string roleId)
        {
            return await _cache.GetAsync(string.Format(CacheKeys.RoleByIdCacheKey, roleId), async () =>
            {
                var role = await _roleManager.FindByIdAsync(roleId).ConfigureAwait(false);
                if (role == null)
                    return new ServiceResult<ApplicationRole>(false, _localizationService.GetResource("RecordNotFound").FormatString(new string[] { ServiceName }).ToStringList(), MessageType.Warning);

                return new ServiceResult<ApplicationRole>(true)
                {
                    Data = role
                };
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Create new role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<ServiceResult<ApplicationRole>> CreateAsync(ApplicationRole role)
        {
            role.Id = Guid.NewGuid().ToString();
            var createResp = await _roleManager.CreateAsync(role).ConfigureAwait(false);

            if (createResp.Succeeded)
            {
                _cache.Remove(CacheKeys.RolesCacheKey);
                return new ServiceResult<ApplicationRole>(true, _localizationService.GetResource("AddNewRecord").FormatString(new string[] { ServiceName }).ToStringList());
            }

            return new ServiceResult<ApplicationRole>(createResp.Succeeded, createResp.Errors, MessageType.Danger);
        }

        /// <summary>
        /// Update role details
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<ServiceResult<ApplicationRole>> UpdateAsync(ApplicationRole role)
        {
            var updateResp = await _roleManager.UpdateAsync(role).ConfigureAwait(false);

            if (updateResp.Succeeded)
            {
                _cache.Remove(CacheKeys.RolesCacheKey);
                _cache.Remove(string.Format(CacheKeys.RoleByIdCacheKey, role.Id));
                _cache.Remove(string.Format(CacheKeys.RoleByNameCacheKey, role.Name));

                return new ServiceResult<ApplicationRole>(true, _localizationService.GetResource("EditRecord").FormatString(new string[] { ServiceName }).ToStringList());
            }

            return new ServiceResult<ApplicationRole>(updateResp.Succeeded, _localizationService.GetResource("RecordNotFound").FormatString(new string[] { ServiceName }).ToStringList(), MessageType.Warning);
        }

        /// <summary>
        /// Delete a role 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IServiceResult> DeleteAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId).ConfigureAwait(false);
            if (role == null)
                return new ServiceResult<ApplicationRole>(false, _localizationService.GetResource("RecordNotFound").FormatString(new string[] { ServiceName }).ToStringList(), MessageType.Warning);

            var deleteResp = await _roleManager.DeleteAsync(roleId).ConfigureAwait(false);
            if (deleteResp.Succeeded)
            {
                _cache.Remove(string.Format(CacheKeys.RoleByIdCacheKey, role.Id));
                _cache.Remove(string.Format(CacheKeys.RoleByNameCacheKey, role.Name));
                _cache.Remove(CacheKeys.RolesCacheKey);
            }

            return new ServiceResult(deleteResp.Succeeded, new List<string> { string.Format(_localizationService.GetResource("DeleteRecord"), ServiceName) });
        }

        #endregion

    }
}
