using StartupProject.Core.Domain.DbEntity;
using StartupProject.EntityFramework.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupProject.EntityFramework.Repository
{
    public class SectionRepository : Repository<Section>
    {
        public SectionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}

