using System;

namespace StartupProject.Core.Security.Identity.User
{
    public class UserCore
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string OfficePhoneNumber { get; set; }

        public string UserImage { get; set; }

        public string UserImageThumbnail { get; set; }

        public string RoleId { get; set; }

        public string RoleName { get; set; }

    }
}
