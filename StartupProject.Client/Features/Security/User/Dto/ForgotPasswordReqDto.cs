using System.ComponentModel.DataAnnotations;

namespace StartupProject.Client.Features.Security.User.Dto
{
    /// <summary>
    /// Change password request with Email
    /// </summary>
    public class ForgotPasswordReqDto
    {
        /// <summary>
        /// Email address
        /// </summary>
        [Required]
        public string Email { get; set; }
    }
}
