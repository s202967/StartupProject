using System;

namespace StartupProject.Core.BaseEntity
{
    public abstract class FullAudited<T> : Entity<T>, IFullAudited
    {
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
