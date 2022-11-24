using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartupProject.Core.Security.Identity
{
    public class AppUser
    {
        public virtual string Id { get; set; }

        public virtual string Email { get; set; }

        public virtual bool EmailConfirmed { get; set; }

        public virtual string Password { get; set; }

        public virtual string SecurityStamp { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual bool PhoneNumberConfirmed { get; set; }

        public virtual bool TwoFactorEnabled { get; set; }

        public virtual bool LockoutEnabled { get; set; }

        public virtual int AccessFailedCount { get; set; }

        public virtual string UserName { get; set; }

        public bool IsInactive { get; set; }

        public string FullName { get; set; }
        public string Affiliation { get; set; }
        public string Country { get; set; }

        public bool IsVerified { get; set; }
        public string UserImage { get; set; }

        public string UserImageThumbnail { get; set; }

        public string MobileNumber { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public List<ApplicationRole> URoles { get; set; }

        public virtual IList<string> Roles { get; set; }

        public virtual ICollection<ApplicationUserClaim> Claims { get; private set; }

        public AppUser()
        {
            URoles = new List<ApplicationRole>();
            Roles = new List<string>();
            Claims = new List<ApplicationUserClaim>();
        }
    }
}
