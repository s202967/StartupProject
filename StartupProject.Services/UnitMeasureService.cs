using PR.Core.Caching;
using PR.Core.Domain.DbEntity;
using PR.Core.Infrastructure.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PR.Services
{
    public class UnitMeasureService : IUnitMeasureService
    {
        private readonly IRepository<UnitMeasure> _unitMeasureRepository;
        private readonly IStaticCacheManager _cache;

        public UnitMeasureService(IRepository<UnitMeasure> unitMeasureRepository, IStaticCacheManager cache)
        {
            _unitMeasureRepository = unitMeasureRepository;
            _cache = cache;
        }

        public async Task<IEnumerable<UnitMeasure>> GetUnitMeasuresAsync()
        {
            return await _cache.Get(CacheKeys.UnitMeasureCacheKey, async () =>
            {
                return await _unitMeasureRepository.GetManyAsync(wh => !wh.IsInactive).ConfigureAwait(false);
            }).ConfigureAwait(false);
        }
    }
}
