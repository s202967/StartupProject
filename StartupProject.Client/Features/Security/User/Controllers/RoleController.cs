using StartupProject.Client.Extension;
using StartupProject.Client.Features.Security.User.Dto;
using StartupProject.Client.Filters;
using StartupProject.Core.ServiceResult;
using StartupProject.Services.Security.User;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace StartupProject.Client.Features.Security.User.Controllers
{
    /// <summary>
    /// Contains user role services
    /// </summary>
    [Route("v1/security/")]
    [ApiController]
    public class RoleController : BasePrivateController
    {
        #region Ctor and properties

        private readonly IRoleService _roleService;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="roleService"></param>
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        #endregion

        #region methods

        /// <summary>
        /// Retrieves all roles
        /// </summary>
        /// <returns></returns>
        [Route("roles")]
        [HttpGet]
        public async Task<ActionResult<ServiceResult<RoleDto>>> GetRolesAsync()
        {
            var serviceResp = await _roleService.GetRolesAsync().ConfigureAwait(false);
            return JOk(serviceResp, serviceResp.Data.Select(x => x.ToDto()));
        }

        /// <summary>
        /// Create a new role
        /// </summary>
        /// <returns></returns>
        [Route("roles")]
        [ServiceFilter(typeof(ValidateDtoAttribute))]
        [ServiceFilter(typeof(UnitOfWorkAttribute))]
        [HttpPost]
        public async Task<ActionResult<ServiceResult<RoleDto>>> CreateAsync(RoleDto dto)
        {
            var entity = dto.ToEntity();
            var serviceResp = await _roleService.CreateAsync(entity).ConfigureAwait(false);
            if (serviceResp.Status) dto.Id = entity.Id;
            return JOk(serviceResp, dto);
        }

        /// <summary>
        /// Retrieve the role by Identifier
        /// </summary>
        /// <param name="id">The id of the role you want to get</param>
        /// <returns>Role with Name,Description,.. field</returns>
        [Route("roles/{id}")]
        [HttpGet]
        public async Task<ActionResult<ServiceResult<RoleDto>>> GetDetailsAsync(string id)
        {
            var serviceResp = await _roleService.FindByIdAsync(id).ConfigureAwait(false);
            return JOk(serviceResp, serviceResp.Data.ToDto());
        }

        /// <summary>
        /// Update a role
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route("roles")]
        [ServiceFilter(typeof(ValidateDtoAttribute))]
        [ServiceFilter(typeof(UnitOfWorkAttribute))]
        [HttpPut]
        public async Task<ActionResult<ServiceResult<RoleDto>>> UpdateAsync(RoleDto dto)
        {
            var entityDetail = await _roleService.FindByIdAsync(dto.Id);
            if (!entityDetail.Status)
                return JOk(entityDetail);

            var entity = dto.ToEntity(entityDetail.Data);
            var serviceResp = await _roleService.UpdateAsync(entity).ConfigureAwait(false);
            return JOk(serviceResp, dto);
        }

        /// <summary>
        /// Delete a role record
        /// </summary>
        /// <param name="id">role Id</param>
        /// <returns></returns>
        [Route("roles/{id}")]
        [ServiceFilter(typeof(UnitOfWorkAttribute))]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var serviceResp = await _roleService.DeleteAsync(roleId: id).ConfigureAwait(false);
            return Ok(serviceResp);
        }

        #endregion

    }
}