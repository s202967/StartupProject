using System;

namespace StartupProject.Core.BaseEntity
{
    public interface IHasCreator
    {
        Guid? CreatedBy { get; set; }
    }
}
