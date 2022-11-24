using StartupProject.Client.Features.Security.User.Dto;
using StartupProject.Core.Infrastructure.Extensions;
using StartupProject.Core.Security.UserActivity;
using StartupProject.Core.ServiceResult;
using StartupProject.Services.Common.Localization;
using StartupProject.Services.Security.User;
using System.Linq;
using System.Threading.Tasks;

namespace StartupProject.Client.Features.Security.User.Factories
{
    /// <summary>
    /// Represents user dto factory
    /// </summary>
    public class UserDtoFactory :  IUserDtoFactory
    {

        #region Ctor/Properties

        private readonly IUserService _userService;
        private readonly ILocalizationService _localizationService;

        /// <summary>
        /// Inject services
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="localizationService"></param>
        /// <param name="clientInfoProvider"></param>
        public UserDtoFactory
        (
            IUserService userService,
            ILocalizationService localizationService,
            IClientInfoProvider clientInfoProvider
        ) 
        {
            _userService = userService;
            _localizationService = localizationService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Ger user list
        /// </summary>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        public async Task<UserListSearchResultDto> GetUserListAsync(UserSearchDto searchDto)
        {
            int total = 0;

            var serviceResult = await _userService.GetUserListAsync(searchDto.UserName, searchDto.Status, searchDto.PageIndex, searchDto.PageSize).ConfigureAwait(false);

            if (serviceResult != null && serviceResult.Any())
            {
                total = serviceResult.First().TotalRows ?? 0;
            }

            return new UserListSearchResultDto
            {
                Data = serviceResult,
                RecordsTotal = total,
                PageIndex = searchDto.PageIndex
            };
        }

        /// <summary>
        /// Enable|disable user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="status">true:enable, false:disable</param>
        /// <returns></returns>
        public async Task<IServiceResult> UpdateUserStatusAsync(string userId, bool status)
        {
            var getUserResp = await _userService.GetUserByUserIdAsync(userId).ConfigureAwait(false);
            if (!getUserResp.Status)
                return new ServiceResult(getUserResp.Status, getUserResp.Message) { MessageType = getUserResp.MessageType };

            getUserResp.Data.IsInactive = !status;
            var serviceResult = await _userService.UpdateAsync(getUserResp.Data).ConfigureAwait(false);
            if (serviceResult.Status)
            {
                return new ServiceResult(true, string.Format(_localizationService.GetResource("User.Status.Changed"), status ? "enabled" : "disabled").ToStringList(), MessageType.Success);
            }
            else
                return serviceResult;
        }

        #endregion

    }
}
