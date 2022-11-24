using StartupProject.Client.Features.Employees.Dto;
using StartupProject.Core.Domain.DbEntity;
using StartupProject.Core.ServiceResult;
using StartupProject.Services.Common.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupProject.Client.Features.Employees.Factory
{
    public class EmployeeFactory : IEmployeeFactory
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeFactory(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        public async Task<ServiceResult<List<EmployeeDTO>>> GetAllEmployeeAndMapToDto()
        {
          var resp =  await _employeeService.GetAllAsync().ConfigureAwait(false);
            var res = resp.Data.Select(empobj => new EmployeeDTO
            {
                EmployeeId = empobj.EmployeeId,
                Name = empobj.Name,
                Email = empobj.Email,
                Position = empobj.Position,

            }).ToList();
            return new ServiceResult<List<EmployeeDTO>>(true) { Data = res };
        }

        public async Task<ServiceResult<EmployeeDTO>> GetEmployeeByIdAndMapToDto(int id)
        {
            var resp = await _employeeService.FindByIdAsync(id).ConfigureAwait(false);
            EmployeeDTO dto = new EmployeeDTO();
            dto.EmployeeId = resp.Data.EmployeeId;
            dto.Name = resp.Data.Name;
            dto.Email = resp.Data.Email;
            dto.Position = resp.Data.Position;
            dto.EmployeeId = resp.Data.EmployeeId;
            return new ServiceResult<EmployeeDTO>(true) { Data = dto};
        }

        public Employee MapEmployeeDTOToEntity(EmployeeDTO dto)
        {
            Employee entity = new Employee();
            entity.EmployeeId = dto.EmployeeId;
            entity.Name = dto.Name;
            entity.Email = dto.Email;
            entity.Position = dto.Position;
            return entity;
        }
    }
}
