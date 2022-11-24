using StartupProject.Core.Domain.DbEntity;
using StartupProject.Core.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupProject.Core.Domain.Interfaces
{
    public interface IEmployeeRepository: IRepository<Employee>
    {

    }
}
