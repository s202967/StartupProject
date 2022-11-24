
using StartupProject.Client.Features.Security.User.Dto;
using StartupProject.Core.Domain.DbEntity;
using StartupProject.Core.Domain.SpEntity;
using StartupProject.Core.Infrastructure.Mapper;
using StartupProject.Core.Security.Identity;
using System.Linq;

namespace StartupProject.Client.Extension
{
    /// <summary>
    /// automapper extension
    /// </summary>
    public static class MappingExtensions
    {
        /// <summary>
        /// map source to destination
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return AutoMapperConfiguration.Mapper.Map<TSource, TDestination>(source);
        }

        /// <summary>
        /// map source to destination
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return AutoMapperConfiguration.Mapper.Map(source, destination);
        }
        
        /// <summary>
        /// Map user dto to entity
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static AppUser ToEntity(this UserDto dto, AppUser user = null)
        {
            if (user == null)
                user = new AppUser();

            user.UserName = dto.UserName;
            user.Email = dto.Email;
            user.FullName = dto.FullName;
            user.PhoneNumber = dto.MobileNumber;
            user.Roles = dto.Roles;

            return user;
        }

        /// <summary>
        /// Map user entity to dto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static UserDto ToDto(this AppUser entity)
        {
            return entity.MapTo<AppUser, UserDto>();
        }

        /// <summary>
        /// Map role dto to entity
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public static ApplicationRole ToEntity(this RoleDto dto, ApplicationRole role = null)
        {
            if (role == null)
                role = new ApplicationRole();

            role.Name = dto.Name;
            return role;
        }

        /// <summary>
        /// Map role entity to dto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static RoleDto ToDto(this ApplicationRole entity)
        {
            return entity.MapTo<ApplicationRole, RoleDto>();
        }

        /// <summary>
        /// Map user entity to user with role dto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static UserWithRoleDto ToUserWithRoleDto(this AppUser entity)
        {
            //return entity.MapTo<AppUser, UserWithRoleDto>();
            return new UserWithRoleDto
            {
                Id = entity.Id,
                FullName = entity.FullName,
                MobileNumber = entity.PhoneNumber,
                Email = entity.Email,
                UserName = entity.UserName,
                Roles = entity.Roles.ToList(),
                UserImage = entity.UserImage,
                UserImageThumbnail = entity.UserImageThumbnail,
                URoles = entity.URoles.Select(x => x.ToDto()).ToList()
            };
        }

    }
}