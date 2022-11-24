using StartupProject.Client.Features.Security.User.Dto;
using StartupProject.Core.ServiceResult;
using System.Threading.Tasks;

namespace StartupProject.Client.Features.Security.User.Factories
{
    /// <summary>
    /// Represents user dto factory
    /// </summary>
    public interface IUserDtoFactory
    {
        /// <summary>
        /// Ger user list
        /// </summary>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        Task<UserListSearchResultDto> GetUserListAsync(UserSearchDto searchDto);

        /// <summary>
        /// Enable|disable user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="status">true:enable, false:disable</param>
        /// <returns></returns>
        Task<IServiceResult> UpdateUserStatusAsync(string userId, bool status);
    }
}
