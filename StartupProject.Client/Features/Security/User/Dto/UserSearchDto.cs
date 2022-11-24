using StartupProject.Client.Features.Common.Dto;

namespace StartupProject.Client.Features.Security.User.Dto
{
    /// <summary>
    /// Represents user search request DTO
    /// </summary>
    public class UserSearchDto : BaseSearchDto
    {
        /// <summary>
        /// User name filter
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// True:active users, False:disabled users
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        public UserSearchDto()
        {
            UserName = "";
            Status = true;
        }
    }
}
