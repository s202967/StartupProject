using System;

namespace StartupProject.Core.BaseEntity
{
    public interface IHasDeleter
    {
        Guid? DeletedBy { get; set; }
    }
}
