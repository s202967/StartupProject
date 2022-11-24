using StartupProject.Core.BaseEntity;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartupProject.EntityFramework.Security.Identity.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationIdentityUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationIdentityUser : IdentityUser<string>, ICreatedOn, IModifiedOn
    {
        public bool IsInactive { get; set; }

        [StringLength(255)]
        public string FullName { get; set; }

        [StringLength(100)]
        public string UserImage { get; set; }

        [StringLength(100)]
        public string UserImageThumbnail { get; set; }

        public string Affiliation { get; set; }
        public string Country { get; set; }
        public string MobileNumber { get; set; }
        public bool IsVerified { get; set; }
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}