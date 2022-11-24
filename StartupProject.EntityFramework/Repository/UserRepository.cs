using StartupProject.Core.BaseEntity;
using StartupProject.Core.Domain.Interfaces;
using StartupProject.Core.Domain.SpEntity;
using StartupProject.EntityFramework.EntityFramework;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace StartupProject.EntityFramework.Repository
{
    public class UserRepository : Repository<NullEntity>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<UserListProcResult>> GetUserListAsync(string userName, bool status, int pageIndex, int pageSize)
        {
            var parameter = new List<SqlParameter>
            {
                new SqlParameter
                {
                    ParameterName = "@userName",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.VarChar,
                    Value = userName
                },
                new SqlParameter
                {
                    ParameterName = "@status",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Bit,
                    Value = status
                },
                new SqlParameter
                {
                    ParameterName = "@pageIndex",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Int,
                    Value = pageIndex
                },
                new SqlParameter
                {
                    ParameterName = "@pageSize",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Int,
                    Value = pageSize
                }
            };

            return await ExecuteReaderAsync<UserListProcResult>("Tab_spGetUserList", CommandType.StoredProcedure, parameter.ToArray()).ConfigureAwait(false);
        }
    }
}
