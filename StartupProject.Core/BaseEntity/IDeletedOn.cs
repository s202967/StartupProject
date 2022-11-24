using System;

namespace StartupProject.Core.BaseEntity
{
    public interface IDeletedOn
    {
        DateTime? DeletedOn { get; set; }
    }
}
