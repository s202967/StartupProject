using System;

namespace StartupProject.Core.BaseEntity
{
    public interface IHasModifier
    {
        Guid? ModifiedBy { get; set; }
    }
}
