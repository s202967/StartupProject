using StartupProject.Client.Features.Employees.Dto;
using StartupProject.Core.Domain.DbEntity;
using StartupProject.Core.ServiceResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StartupProject.Client.Features.Employees.Factory
{
    public interface IEmployeeFactory
    {
        Task<ServiceResult<List<EmployeeDTO>>> GetAllEmployeeAndMapToDto();
        Task<ServiceResult<EmployeeDTO>> GetEmployeeByIdAndMapToDto(int id);
        Employee MapEmployeeDTOToEntity(EmployeeDTO dto);
    }
}
