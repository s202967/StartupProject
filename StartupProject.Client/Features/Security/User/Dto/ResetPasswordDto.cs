using System.ComponentModel.DataAnnotations;

namespace StartupProject.Client.Features.Security.User.Dto
{
    /// <summary>
    ///ResetPassword Dto with UserId, Password, Code and Description fields.
    /// </summary>
    public class ResetPasswordDto
    {
        /// <summary>
        /// User id
        /// </summary>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Confirm password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}