using System;

namespace StartupProject.Core.Domain.SpEntity
{
    public class UserListProcResult
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string OfficePhoneNumber { get; set; }

        public string Roles { get; set; }

        public bool Status { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public int? TotalRows { get; set; }

        public long? RowNumber { get; set; }
    }
}
