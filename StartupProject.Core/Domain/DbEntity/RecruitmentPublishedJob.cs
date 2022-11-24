using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StartupProject.Core.Domain.DbEntity
{
    [Table("Recruitment_PublishedJob")]
    public class RecruitmentPublishedJob
    {
        [Key]
        public int Id { get; set; }
        public int RecruitmentJobId { get; set; }
        public string InternalJobTitle { get; set; }
        public string ExternalJobTitle { get; set; }
        public string Department { get; set; }
        public string Branch { get; set; }
        public string EmploymentType { get; set; }
        public int? NumberOfOpenings { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public DateTime? StartDateEng { get; set; }
        public DateTime? EndDateEng { get; set; }
        public bool IsPayShow { get; set; }
        public decimal? SalaryRangeFrom { get; set; }
        public decimal? SalaryRangeTo { get; set; }
        public byte? SalaryDurationType { get; set; }
        public bool IsExperience { get; set; }
        public int? Experience { get; set; }
        public byte? ExperienceDurationType { get; set; }
        public string JobDescription { get; set; }
        public string Tags { get; set; }
        public string PublishedDate { get; set; }
        public DateTime? PublishedDateEng { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }
}
