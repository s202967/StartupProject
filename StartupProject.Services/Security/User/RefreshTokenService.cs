using StartupProject.Core.Infrastructure.DataAccess;
using StartupProject.Core.Infrastructure.Helper;
using StartupProject.Core.Security.Identity;
using StartupProject.Core.ServiceResult;
using StartupProject.Services.Security.User;
using System.Linq;
using System.Threading.Tasks;

namespace IRC.Services.Security.User
{
    /// <summary>
    /// Refresh token services
    /// </summary>
    public class RefreshTokenService : IRefreshTokenService
    {

        #region Ctor/properties

        private readonly IRepository<RefreshToken> _repository;

        public RefreshTokenService(IRepository<RefreshToken> repository)
        {
            _repository = repository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create refresh token
        /// </summary>
        public async Task<ServiceResult<RefreshToken>> CreateAsync(RefreshToken entity)
        {
            entity.CreatedOn = DateTimeFormatter.CurrentDateTimeUtc;
            await _repository.AddAsync(entity).ConfigureAwait(false);
            await _repository.CommitAsync().ConfigureAwait(false);
            return new ServiceResult<RefreshToken>(true);
        }

        /// <summary>
        /// Update refresh token
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<ServiceResult<RefreshToken>> UpdateAsync(RefreshToken entity)
        {
            entity.CreatedOn = DateTimeFormatter.CurrentDateTimeUtc;
            _repository.Update(entity);
            await _repository.CommitAsync().ConfigureAwait(false);
            return new ServiceResult<RefreshToken>(true);
        }

        /// <summary>
        /// Get refresh token by user name
        /// </summary>
        /// <param name="username"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public RefreshToken GetByUserName(string username, string ipAddress)
        {
            var entity = _repository.Table.FirstOrDefault(x => x.UserName == username && x.ClientIpAddress == ipAddress);
            if (entity == null)
                return null;
            return entity;
        }

        /// <summary>
        /// Get refresh token by token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public ServiceResult<RefreshToken> GetByToken(string refreshToken, string ipAddress)
        {
            var entity = _repository.Table.FirstOrDefault(x => x.Token == refreshToken && x.ClientIpAddress == ipAddress);
            if (entity == null)
                return new ServiceResult<RefreshToken>(false);
            return new ServiceResult<RefreshToken>(true) { Data = entity };
        }

        /// <summary>
        /// Delete token
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<ServiceResult<RefreshToken>> DeleteAsync(string username, string ipAddress)
        {
            var entity = _repository.Table.FirstOrDefault(x => x.UserName == username && x.ClientIpAddress == ipAddress);
            if (entity != null)
            {
                _repository.Delete(entity);
                await _repository.CommitAsync().ConfigureAwait(false);
            }

            return new ServiceResult<RefreshToken>(true);
        }

        #endregion

    }
}
