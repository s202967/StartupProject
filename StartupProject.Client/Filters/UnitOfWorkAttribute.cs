using StartupProject.Services.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StartupProject.Client.Filters
{
    /// <summary>
    /// Unit of work handler
    /// </summary>
    public class UnitOfWorkAttribute : ActionFilterAttribute
    {
        private readonly ITransactionManager _helper;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="helper"></param>
        public UnitOfWorkAttribute(ITransactionManager helper)
        {
            _helper = helper;
        }

        /// <summary>
        /// Begin transaction
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _helper.BeginTransaction();
        }

        /// <summary>
        /// End transaction
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            _helper.EndTransaction(actionExecutedContext);
            _helper.CloseSession();
        }
    }
}