using System;

namespace StartupProject.Core.BaseEntity
{
    public interface IModifiedOn
    {
        DateTime? ModifiedOn { get; set; }
    }
}
