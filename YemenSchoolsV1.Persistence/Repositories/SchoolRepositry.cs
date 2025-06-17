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
    public class SchoolRepositry : GenericRepositoryAsync<School>, ISchoolRepositry
    {
        private readonly YemenShoolsDbContext dbContext;

        public SchoolRepositry(YemenShoolsDbContext dbContext) :base(dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task CreateSchoolPhonesRangAsync(List<SchoolPhone> schoolPhones)
        {
           await dbContext.schoolPhones.AddRangeAsync(schoolPhones);
            await dbContext.SaveChangesAsync();
        }

        public async Task<School?> GetSchoolDetailsInculdeAsync(Guid schoolId)
        {
            return await dbContext.Schools.Include(c=>c.City).Include(r=>r.Region).Include(ph=>ph.SchoolPhones).FirstOrDefaultAsync(s=>s.Id==schoolId);
           
        }

        public IQueryable<School> GetSchoolsWithCityAndRegionQueryable()
        {
            return GetTableNoTracking()
         .Include(s => s.City)
         .Include(s => s.Region);

        }
    }
}
