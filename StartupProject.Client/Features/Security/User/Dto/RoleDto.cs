using System.ComponentModel.DataAnnotations;

namespace StartupProject.Client.Features.Security.User.Dto
{
    /// <summary>
    /// Represents a user role dto
    /// </summary>
    public class RoleDto
    {
        /// <summary>
        /// Role id | identifier
        /// </summary>
        [StringLength(450)]
        public string Id { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        [Required, StringLength(256)]
        public string Name { get; set; }

    }
}
