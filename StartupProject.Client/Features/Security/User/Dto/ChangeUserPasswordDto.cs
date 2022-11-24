using System.ComponentModel.DataAnnotations;

namespace StartupProject.Client.Features.Security.User.Dto
{
    /// <summary>
    /// Change other User Password from Admin account
    /// </summary>
    public class ChangeUserPasswordDto
    {
        /// <summary>
        /// User identifier
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// New password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
