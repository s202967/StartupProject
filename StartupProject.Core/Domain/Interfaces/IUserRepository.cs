using StartupProject.Core.BaseEntity;
using StartupProject.Core.Domain.SpEntity;
using StartupProject.Core.Infrastructure.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StartupProject.Core.Domain.Interfaces
{
    public interface IUserRepository : IRepository<NullEntity>
    {
        Task<IEnumerable<UserListProcResult>> GetUserListAsync(string userName, bool status, int pageIndex, int pageSize);
    }
}
