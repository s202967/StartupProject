using StartupProject.Core.BaseEntity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartupProject.Core.Security.UserActivity
{
    [Table("ActivityLog")]
    public class ActivityLogss : Entity<long>, ICreatedOn
    {
        /// <summary>
        /// Activity log type
        /// </summary>
        [StringLength(50)]
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        [StringLength(256)]
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// USer Id
        /// </summary>
        [NotMapped]
        public string UserId { get; set; }

        /// <summary>
        /// Execution Duration
        /// </summary>
        [NotMapped]
        public int ExecutionDuration { get; set; }

        /// <summary>
        ///Created date
        /// </summary>
        [Required]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// IP address of the client.
        /// </summary>
        [Required]
        public string ClientIpAddress { get; set; }

        /// <summary>
        /// Service (class/interface) name.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Table name
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Service (class/interface) name.
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Browser Info
        /// </summary>
        public string BrowserInfo { get; set; }

        public override string ToString()
        {
            return string.Format(
                "AUDIT LOG: {0} is executed by user {1} in {2} ms from {3} IP address.",
                 ServiceName, UserId, ExecutionDuration, ClientIpAddress
                );
        }
    }
}
