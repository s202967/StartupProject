using StartupProject.Core.Domain.DbEntity;
using StartupProject.Core.ServiceResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupProject.Services.Common.Service
{
    public interface IEmployeeService
    {
        Task<ServiceResult<List<Employee>>> GetAllAsync();
        Task<ServiceResult<Employee>> FindByIdAsync(int id);
        Task<bool> CreateAsync(Employee employee);
        Task <bool> EditAsync(Employee employee);
        Task <bool> DeleteAsync(int id);
    }
}
