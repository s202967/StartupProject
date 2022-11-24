using PR.Core.Domain.DbEntity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PR.Services
{
    public interface IUnitMeasureService
    {
        Task<IEnumerable<UnitMeasure>> GetUnitMeasuresAsync();
    }
}
