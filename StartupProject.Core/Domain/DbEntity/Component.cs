using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupProject.Core.Domain.DbEntity
{
    [Table("Components")]
    public class Component
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
