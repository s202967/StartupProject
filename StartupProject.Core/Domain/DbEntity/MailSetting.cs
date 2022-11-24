using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartupProject.Core.Domain.DbEntity
{
    [Table("MailSetting")]
    public class MailSetting

    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string FromEmail { get; set; }

        [StringLength(100)]
        public string HostName { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        public int? Port { get; set; }

        public bool EnableSsl { get; set; }

        public bool? IsAmazonEmailService { get; set; }
    }
}
