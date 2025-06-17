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
    public class RegionRepositry : GenericRepositoryAsync<Region>, IRegionRepositry
    {
        private readonly YemenShoolsDbContext dbContext;

        public RegionRepositry(YemenShoolsDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Region>?> GetRegionByCityIdIncludeAsync(Guid cityId)
        {
            return await dbContext.Regions.Where(e=>e.CityId == cityId).Include(r=>r.City).ToListAsync();
        }
        public async Task<int?> GetSchoolCount(Guid regionId)
        {
            return await dbContext.Schools.CountAsync(s => s.RegionId == regionId);
        }
    }
}
