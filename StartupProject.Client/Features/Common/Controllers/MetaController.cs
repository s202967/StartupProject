using Microsoft.AspNetCore.Mvc;
using StartupProject.Services.Common.Settings;
using System.Threading.Tasks;

namespace StartupProject.Client.Features.Common.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("v1/meta/")]
    public class MetaController : BasePrivateController
    {

        #region Ctor/Properties
        private readonly ICommonService _commonService;
        /// <summary>
        /// Inject related services
        /// </summary>
        public MetaController(ICommonService commonService)
        {
            _commonService = commonService;
        }

        #endregion

        #region Public API
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("templates")]
        [HttpGet]
        public async Task<IActionResult> GetTemplates()
        {
            var countries = await _commonService.GetTemplates().ConfigureAwait(false);
            return Ok(countries);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("sections")]
        [HttpGet]
        public async Task<IActionResult> GetSections()
        {
            var countries = await _commonService.GetSections().ConfigureAwait(false);
            return Ok(countries);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("checklist")]
        [HttpGet]
        public async Task<IActionResult> GetCheckList()
        {
            var countries = await _commonService.GetCheckList().ConfigureAwait(false);
            return Ok(countries);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("components")]
        [HttpGet]
        public async Task<IActionResult> GetComponents()
        {
            var countries = await _commonService.GetComponents().ConfigureAwait(false);
            return Ok(countries);
        }
        #endregion
    }
}