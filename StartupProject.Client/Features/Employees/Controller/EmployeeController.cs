using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StartupProject.Client.Features.Employees.Dto;
using StartupProject.Client.Features.Employees.Factory;
using StartupProject.Core.Domain.DbEntity;
using StartupProject.Core.ServiceResult;
using StartupProject.Services.Common.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupProject.Client.Features.Employees.Controller
{
    [Authorize]
    [Route("v1/Employee/")]
    [ApiController]

    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeFactory _employeeFactory;

        public EmployeeController(IEmployeeService employeeService, IEmployeeFactory employeeFactory)
        {
            _employeeService = employeeService;
            _employeeFactory = employeeFactory;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var res = await _employeeFactory.GetAllEmployeeAndMapToDto().ConfigureAwait(false);
            return Ok(res);
        }

        [Route("{id}")]
        [HttpGet]

        public async Task<ActionResult> GetAsync(int id)
        {
            return Ok(await _employeeFactory.GetEmployeeByIdAndMapToDto(id).ConfigureAwait(false));
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(EmployeeDTO empdto)
        {
            var data = _employeeFactory.MapEmployeeDTOToEntity(empdto);
            var resp = await _employeeService.CreateAsync(data).ConfigureAwait(false);
            if (resp == true) return Ok(new ServiceResult(true, new List<string>() { "Created Sucessfully","ABC" }, null));
            else    return Ok(new ServiceResult(false, new List<string>() { "Failed to create!" }, null));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(EmployeeDTO empDTO)
        {
            var data = _employeeFactory.MapEmployeeDTOToEntity(empDTO);
            var res = await _employeeService.EditAsync(data).ConfigureAwait(false);
            if (res == true) return Ok(new ServiceResult(true, new List<string>() { "Update Successfully" }, null));
            else    return Ok(new ServiceResult(false, new List<string>() { "Failed to Update" }, null));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResult>> DeleteAsync(int id)
        {
            var resp = await _employeeService.DeleteAsync(id).ConfigureAwait(false);
            if(resp == true)  return Ok(new ServiceResult(true, new List<string>() { "Deleted Successfully" }, null));
            else    return Ok(new ServiceResult(false, new List<string>() { "Failed to delete!" }, null));
        }


    }


}
