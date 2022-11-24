using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StartupProject.Core.Domain.DbEntity
{
    [Table("Recruitment_PublishedJob_ApplicationForm")]
    public class RecruitmentPublishedJobApplicationForm
    {
        [Key]
        public int Id { get; set; }
        public int ComponentRef_Id { get; set; }
        public int PublishedJobRef_Id { get; set; }
        public int RequiredType { get; set; }
    }
}
