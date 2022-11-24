using System.Collections.Generic;

namespace StartupProject.Core.Security.Identity
{
    public class ApplicationRole
    {
        public string Id { get; set; }

        public virtual ICollection<ApplicationUserRole> Users { get; private set; }

        public string Name { get; set; }

        public int RoleId { get; set; }

    }
}
