namespace StartupProject.Core.Security.Identity
{
    public class ApplicationUserRole
    {
        public virtual string UserId { get; set; }

        public virtual string RoleId { get; set; }

        public string RoleName { get; set; }
    }
}
