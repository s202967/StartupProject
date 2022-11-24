using System.Collections.Generic;
using System.Threading.Tasks;
using StartupProject.Core.Domain;
using StartupProject.Core.Domain.DbEntity;

namespace StartupProject.Services.Common.Settings
{
    public interface ICommonService
    {
        Task<IEnumerable<Template>> GetTemplates();
        Task<IEnumerable<Section>> GetSections();
        Task<IEnumerable<CheckList>> GetCheckList();
        Task<IEnumerable<Component>> GetComponents();
    }
}