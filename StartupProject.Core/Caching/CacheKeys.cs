namespace StartupProject.Core.Caching
{
    /// <summary>
    /// Represents default values related to directory services
    /// </summary>
    public static class CacheKeys
    {
        /// <summary>
        /// Country
        /// </summary>
        public static string CountryCacheKey => "Country";

        /// <summary>
        /// Province
        /// </summary>
        public static string ProvinceCacheKey => "Province";

        /// <summary>
        /// Directorate
        /// </summary>
        public static string DirectorateCacheKey => "Directorate";

        /// <summary>
        /// District
        /// </summary>
        public static string DistrictCacheKey => "District";

        /// <summary>
        /// Asset group
        /// </summary>
        public static string AssetGroupCacheKey => "AssetGroup";

        /// <summary>
        /// Asset sub-group
        /// </summary>
        public static string AssetSubGroupCacheKey => "AssetSubGroup";

        /// <summary>
        /// Asset sub-group by grp code
        /// </summary>
        public static string AssetSubGroupByGrpCodeCacheKey => "AssetSubGroup.ByGrpCode-{0}";

        /// <summary>
        /// Department
        /// </summary>
        public static string DepartmentCacheKey => "Department";

        /// <summary>
        /// Office
        /// </summary>
        public static string OfficeCacheKey => "Office";

        /// <summary>
        /// UnitMeasure
        /// </summary>
        public static string UnitMeasureCacheKey => "UnitMeasure";

        /// <summary>
        /// Asset sub-code
        /// </summary>
        public static string AssetSubCodeCacheKey => "AssetSubCode";

        /// <summary>
        /// Asset sub-code by grp code by sub-grp code
        /// </summary>
        public static string AssetSubCodeByGrpCodeBySubGrpCode => "AssetSubCode.ByGrpCode-{0}.BySubGrpCode-{1}";

        /// <summary>
        /// Office type
        /// </summary>
        public static string OfficeTypeCacheKey => "OfficeType";

        /// <summary>
        /// Asset condition
        /// </summary>
        public static string AssetConditionCacheKey => "AssetCondition";

        /// <summary>
        /// Roles
        /// </summary>
        public static string RolesCacheKey => "Roles.All";

        /// <summary>
        /// Role by name
        /// </summary>
        public static string RoleByNameCacheKey => "Role.ByName-{0}";

        /// <summary>
        /// Role by id
        /// </summary>
        public static string RoleByIdCacheKey => "Role.ById-{0}";

        /// <summary>
        /// User by username
        /// </summary>
        public static string UserByUserNameCacheKey => "User.ByUserName-{0}";

        /// <summary>
        /// Users
        /// </summary>
        public static string UsersCacheKey => "Users.All";
    }
}