using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
    public class SchoolNewsRepositry : GenericRepositoryAsync<SchoolNews>, ISchoolNewsRepositry
    {
   
        private readonly YemenShoolsDbContext dbContext;

        public SchoolNewsRepositry(YemenShoolsDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<SchoolNews>?> GetSchoolNewsBySchoolIdAsync(Guid schoolId)
        {
            return await dbContext.SchoolNews.Where(e => e.SchoolId == schoolId).ToListAsync();
        }
    }
}
