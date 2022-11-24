using StartupProject.Core.BaseEntity;

namespace StartupProject.Core.Infrastructure.Localization
{
    /// <summary>
    /// Represents a locale string resource
    /// </summary>
    public class LocaleStringResource : Entity<int>
    {
        /// <summary>
        /// Gets or sets the resource name
        /// </summary>
        public string ResourceName { get; set; }

        /// <summary>
        /// Gets or sets the resource value
        /// </summary>
        public string ResourceValue { get; set; }
    }
}
