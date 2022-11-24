namespace StartupProject.Client.Features.Security.User.Dto
{
    /// <summary>
    /// Represents user input dto
    /// </summary>
    public class CreateUserDto : UserDto
    {
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }
}
