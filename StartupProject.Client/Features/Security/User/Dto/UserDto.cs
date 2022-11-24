using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StartupProject.Client.Features.Security.User.Dto
{
    /// <summary>
    /// Represents user dto
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Id | identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Full Name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Mobile number
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Office phone number
        /// </summary>
        public string OfficePhoneNumber { get; set; }

        /// <summary>
        /// Email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// User image
        /// </summary>
        public IFormFile UserImage { get; set; }

        /// <summary>
        /// User image thumbnail
        /// </summary>
        public IFormFile UserImageThumbnail { get; set; }

        /// <summary>
        /// Comma separated approval role directorate ids
        /// </summary>
        public string ApprovalRoleDirectorateRefIds { get; set; }

        /// <summary>
        /// Comma separated approval role province ids
        /// </summary>
        public string ApprovalRoleProvinceRefIds { get; set; }

        /// <summary>
        /// Role names
        /// </summary>
        public virtual List<string> Roles { get; set; }
    }
}
