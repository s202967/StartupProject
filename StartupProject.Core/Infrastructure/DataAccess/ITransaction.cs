using System;

namespace StartupProject.Core.Infrastructure.DataAccess
{
    public interface ITransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}