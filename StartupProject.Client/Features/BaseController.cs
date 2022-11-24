using StartupProject.Core.Infrastructure;
using StartupProject.Core.Infrastructure.Extensions;
using StartupProject.Core.ServiceResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace StartupProject.Client.Features
{
    /// <summary>
    /// Base class with resulting methods
    /// </summary>
    [ApiController]
    public abstract class BaseController : ControllerBase
    {

        /// <summary>
        /// Logger instance
        /// </summary>
        private ILogger _logger;
        ///// <summary>
        ///// ctor
        ///// </summary>
        //public BaseController(ILogger logger)
        //{
        //    _logger = logger;
        //}

        /// <summary>
        /// Log service response
        /// </summary>
        /// <param name="serviceResp"></param>
        private void LogResponse(IServiceResult serviceResp)
        {
            if (serviceResp.Message == null) return;

            _logger = EngineContext.Current.Resolve<ILogger<BaseController>>();

            if (serviceResp.MessageType == MessageType.Success || serviceResp.MessageType == MessageType.Info)
                _logger.LogInformation(",".SmartJoin(serviceResp.Message.ToArray()));

            else if (serviceResp.MessageType == MessageType.Warning)
                _logger.LogWarning(",".SmartJoin(serviceResp.Message.ToArray()));

            else
                _logger.LogError(",".SmartJoin(serviceResp.Message.ToArray()));
        }

        /// <summary>
        /// Ok responses
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resp"> </param>
        /// <returns>Ok response</returns>
        protected OkObjectResult JOk<T>(T resp) where T : IServiceResult
        {
            LogResponse(resp);
            return Ok(resp);
        }

        /// <summary>
        /// Ok responses
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resp"> </param>
        /// <param name="dto"></param>
        /// <returns>Ok response</returns>
        protected OkObjectResult JOk<T>(T resp, object dto) where T : ServiceResult
        {
            var response = new ServiceResult<object>(resp.Status, resp.Message, resp.MessageType)
            {
                Data = dto,
                MessageType = resp.MessageType
            };
            LogResponse(response);
            return Ok(response);
        }

    }
}