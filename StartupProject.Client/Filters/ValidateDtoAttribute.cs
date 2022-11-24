using StartupProject.Client.Extension.System_;
using StartupProject.Core.Infrastructure.Extensions;
using StartupProject.Core.ServiceResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace StartupProject.Client.Filters
{
    /// <summary>
    /// Attribute for DTO validation
    /// </summary>
    public class ValidateDtoAttribute : ActionFilterAttribute
    {
        private readonly ILogger _logger;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="logger"></param>
        public ValidateDtoAttribute(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger("ValidateModelAttribute");
        }

        /// <summary>
        /// Validate Dto
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var message = context.ModelState.GetModelErrors();
                _logger.LogInformation(",".SmartJoin(message.ToArray()));
                context.Result = new OkObjectResult(new ServiceResult(false, message, MessageType.ValidationFailed));
            }
        }
    }
}
