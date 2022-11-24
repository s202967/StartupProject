
using System.Collections.Generic;

namespace StartupProject.Client.Features.Security.User.Dto
{
    /// <summary>
    /// User with roles info
    /// </summary>
    public class UserWithRoleDto : UserDto
    {
        /// <summary>
        /// User image
        /// </summary>
        new public string UserImage { get; set; }

        /// <summary>
        /// User image thumbnail
        /// </summary>
        new public string UserImageThumbnail { get; set; }

        /// <summary>
        /// User Roles
        /// </summary>
        public List<RoleDto> URoles { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        public UserWithRoleDto()
        {
            URoles = new List<RoleDto>();
        }
    }
}
