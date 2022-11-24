using Microsoft.AspNetCore.Mvc.Filters;

namespace StartupProject.Services.Helpers
{
    public interface ITransactionManager
    {
        void BeginTransaction();

        void EndTransaction(ActionExecutedContext filterContext);

        void EndTransaction();

        void CloseSession();
    }
}