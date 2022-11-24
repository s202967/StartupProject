using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartupProject.Core.Domain.DbEntity
{
    [Table("EmailContent")]
    public class EmailContent
    {
        [Key]
        public long Id { get; set; }

        [StringLength(200)]
        public string Subject { get; set; }

        [StringLength(int.MaxValue)]
        public string Body { get; set; }

        public int Type { get; set; }
    }
}
