using AutoMapper;
using StartupProject.Client.Features.Security.User.Dto;
using StartupProject.Core.Domain.DbEntity;
using StartupProject.Core.Domain.SpEntity;
using StartupProject.Core.Infrastructure.Mapper;
using StartupProject.Core.Security.Identity;

namespace StartupProject.Client.Configuration.Mapper
{
    /// <summary>
    /// AutoMapper configuration for admin area models
    /// </summary>
    public class AdminMapperConfiguration : Profile, IMapperProfile
    {
        /// <summary>
        /// ctor
        /// </summary>
        public AdminMapperConfiguration()
        {
            //Role            
            CreateMap<RoleDto, ApplicationRole>();
            CreateMap<ApplicationRole, RoleDto>();

            //User      
            CreateMap<UserDto, AppUser>();
            CreateMap<AppUser, UserDto>();
            CreateMap<AppUser, UserWithRoleDto>();
        }

        /// <summary>
        /// Order of this mapper implementation
        /// </summary>
        public int Order => 0;
    }
}