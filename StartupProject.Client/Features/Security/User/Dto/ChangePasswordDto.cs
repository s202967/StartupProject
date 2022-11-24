using System.ComponentModel.DataAnnotations;

namespace StartupProject.Client.Features.Security.User.Dto
{
    /// <summary>
    /// Change password request with current password, new password
    /// </summary>
    public class ChangePasswordDto
    {
        /// <summary>
        /// Current password
        /// </summary>
        [Required]
        public string CurrentPassword { get; set; }

        /// <summary>
        /// New password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        /// <summary>
        /// Confirm new password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }

    }
}