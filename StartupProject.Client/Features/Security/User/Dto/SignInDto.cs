namespace StartupProject.Client.Features.Security.User.Dto
{
    /// <summary>
    /// Sign in request DTO
    /// </summary>
    public class SignInDto
    {
        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }
}
