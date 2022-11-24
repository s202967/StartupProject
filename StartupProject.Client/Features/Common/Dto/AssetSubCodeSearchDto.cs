namespace StartupProject.Client.Features.Common.Dto
{
    /// <summary>
    /// Represents asset sub code search request DTO
    /// </summary>
    public class AssetSubCodeSearchDto
    {
        /// <summary>
        /// Asset group code
        /// </summary>
        public int? GrpCode { get; set; }

        /// <summary>
        /// Asset sub-group code
        /// </summary>
        public int? SubGrpCode { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        public AssetSubCodeSearchDto()
        {
            GrpCode = null;
            SubGrpCode = null;
        }
    }
}
