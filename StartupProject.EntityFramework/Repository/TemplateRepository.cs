using StartupProject.Core.Domain.DbEntity;
using StartupProject.EntityFramework.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupProject.EntityFramework.Repository
{
    public class TemplateRepository : Repository<Template>
    {
        public TemplateRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
