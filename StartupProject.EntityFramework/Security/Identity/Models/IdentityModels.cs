using Microsoft.AspNetCore.Identity;

namespace StartupProject.EntityFramework.Security.Identity.Models
{
    public class ApplicationIdentityRole : IdentityRole<string>
    {
        public ApplicationIdentityRole()
        {
        }

        public ApplicationIdentityRole(string name)
        {
            Name = name;
        }

      
    }

    public class ApplicationIdentityUserRole : IdentityUserRole<string>
    {
    }

    public class ApplicationIdentityUserClaim : IdentityUserClaim<string>
    {
    }

    public class ApplicationIdentityUserLogin : IdentityUserLogin<string>
    {
    }
}