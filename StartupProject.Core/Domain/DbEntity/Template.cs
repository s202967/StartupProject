using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupProject.Core.Domain.DbEntity
{
    [Table("Template")]
    public class Template
    {
        [Key]
        public int Id { get; set; }
        public string TemplateKey { get; set; }
        public string TemplateTitle { get; set; }
        public string TemplateText { get; set; }
    }
}
