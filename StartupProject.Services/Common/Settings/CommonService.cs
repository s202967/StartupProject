using StartupProject.Core.Domain;
using StartupProject.Core.Domain.DbEntity;
using StartupProject.Core.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupProject.Services.Common.Settings
{
    public class CommonService : ICommonService
    {
        #region ctor/properties
        private readonly IRepository<Template> _templateRepository;
        private readonly IRepository<Section> _sectionRepository;
        private readonly IRepository<CheckList> _checkListRepository;
        private readonly IRepository<Component> _componentRepository;
        public CommonService(IRepository<Template> templateRepository,
            IRepository<Section> sectionRepository,
            IRepository<CheckList> checkListRepository,
            IRepository<Component> componentRepository)
        {
            _templateRepository = templateRepository;
            _sectionRepository = sectionRepository;
            _checkListRepository = checkListRepository;
            _componentRepository = componentRepository;
        }
        #endregion



        public async Task<IEnumerable<Template>> GetTemplates()
        {
            var ls = await _templateRepository.GetAllAsync().ConfigureAwait(false);
            return ls;
        }
        public async Task<IEnumerable<Section>> GetSections()
        {
            var ls = await _sectionRepository.GetAllAsync().ConfigureAwait(false);
            return ls;
        }
        public async Task<IEnumerable<CheckList>> GetCheckList()
        {
            var ls = await _checkListRepository.GetAllAsync().ConfigureAwait(false);
            return ls;
        }
        public async Task<IEnumerable<Component>> GetComponents()
        {
            var ls = await _componentRepository.GetAllAsync().ConfigureAwait(false);
            return ls;
        }

        
    }
}
