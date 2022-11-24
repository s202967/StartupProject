namespace StartupProject.Services.Security.User
{
    public class SignInResponse
    {
        public string Username { get; set; }

        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public int ExpireMinutes { get; set; }

        public int IdleTimeoutMinutes { get; set; }
    }
}