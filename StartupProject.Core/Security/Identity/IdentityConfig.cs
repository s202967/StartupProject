namespace StartupProject.Core.Security.Identity
{
    public class IdentityConfig
    {
        public PasswordElement Password { get; set; }

        public UserElement User { get; set; }

        public LockoutElement Lockout { get; set; }

        public CustomElement Custom { get; set; }

        public ApiToken ApiToken { get; set; }
    }

    public class PasswordElement
    {
        public int RequiredLength { get; set; }

        public bool RequireLowercase { get; set; }

        public bool RequireUppercase { get; set; }

        public bool RequireDigit { get; set; }

        public bool RequireNonAlphanumeric { get; set; }
    }

    public class UserElement
    {
        public bool RequireUniqueEmail { get; set; }
    }

    public class LockoutElement
    {
        public bool AllowedForNewUsers { get; set; }

        public int DefaultLockoutTimeSpanInMins { get; set; }

        public int MaxFailedAccessAttempts { get; set; }
    }

    public class CustomElement
    {
        public bool PreventMultipleLoginForSameUser { get; set; }

        public int UserIdleTimeoutMinutes { get; set; }
    }

    public class ApiToken
    {
        public string SecretKey { get; set; }

        public int AccessExpireMinutes { get; set; }

        public int RefreshExpireMinutes { get; set; }
    }
}
