using StartupProject.Core.Domain.DbEntity;
using StartupProject.EntityFramework.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupProject.EntityFramework.Repository
{
    public class CheckListRepository : Repository<CheckList>
    {
        public CheckListRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}

