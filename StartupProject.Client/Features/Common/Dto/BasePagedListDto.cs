using StartupProject.Core.ServiceResult;
using System.Collections.Generic;

namespace StartupProject.Client.Features.Common.Dto
{
    /// <summary>
    /// base class for paged list item
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BasePagedListDto<T> : IServiceResult
    {
        /// <summary>
        /// Gets or sets data records
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// Page Index
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Gets or sets a number of total data records
        /// </summary>
        public int RecordsTotal { get; set; }

        /// <summary>
        /// Service response status
        /// </summary>
        public bool Status { get; set; } = true;

        /// <summary>
        /// message 
        /// </summary>
        public List<string> Message { get; set; }

        /// <summary>
        /// message type
        /// </summary>
        public string MessageType { get; set; } = Core.ServiceResult.MessageType.Success.ToString();
    }
}