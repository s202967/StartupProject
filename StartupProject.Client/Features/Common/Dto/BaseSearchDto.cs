namespace StartupProject.Client.Features.Common.Dto
{
    /// <summary>
    /// Represents base search model
    /// </summary>
    public class BaseSearchDto
    {
        private int _pageSize = 20;

        /// <summary>
        /// Page index
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value > 0)
                    _pageSize = value;
            }
        }
    }
}