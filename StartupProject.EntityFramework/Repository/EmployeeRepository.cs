using StartupProject.Core.Domain.DbEntity;
using StartupProject.Core.Domain.Interfaces;
using StartupProject.EntityFramework.EntityFramework;

namespace StartupProject.EntityFramework.Repository
{
    public class EmployeeRepository: Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
