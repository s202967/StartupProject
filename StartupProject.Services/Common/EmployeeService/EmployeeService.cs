using Microsoft.Extensions.Logging;
using StartupProject.Core.Domain.DbEntity;
using StartupProject.Core.Infrastructure.DataAccess;
using StartupProject.Core.ServiceResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupProject.Services.Common.Service
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly ILogger _logger;


        public EmployeeService(IRepository<Employee> employeeRepository, ILoggerFactory loggerFactory)
        {
            _employeeRepository = employeeRepository;
            _logger = loggerFactory.CreateLogger("EmployeeService");

        }

        public async Task<ServiceResult<List<Employee>>> GetAllAsync()
        {
            var resp = await _employeeRepository.GetAllAsync().ConfigureAwait(false);
            return new ServiceResult<List<Employee>>(true) { Data = resp.ToList()};

        }

        public async Task<ServiceResult<Employee>> FindByIdAsync(int id )
        {
            return new ServiceResult<Employee>(true) { Data = await _employeeRepository.GetByIdAsync(id).ConfigureAwait(false) };

        }

        public async Task<bool> CreateAsync(Employee employee)
        {
            try
            {
                employee.CreatedOn = DateTime.Now;
                await _employeeRepository.AddAsync(employee);
                _employeeRepository.Commit();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
            return true;

        }

        public async Task<bool> EditAsync(Employee employee)
        {
            try
            {
                employee.ModifiedOn = DateTime.Now;
                _employeeRepository.Update(employee);
                await _employeeRepository.CommitAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
            return true;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var res = await _employeeRepository.GetByIdAsync(id).ConfigureAwait(false);
            if (res != null)
            {
                _employeeRepository.Delete(res);
                _employeeRepository.Commit();
                return true;
            }
            return false;
        }
    }
}
