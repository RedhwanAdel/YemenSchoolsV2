using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
    public class SchoolRepositry : GenericRepositoryAsync<School>, ISchoolRepositry
    {
        private readonly YemenShoolsDbContext dbContext;

        public SchoolRepositry(YemenShoolsDbContext dbContext) : base(dbContext)
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
            return await dbContext.Schools.Include(c => c.City).Include(r => r.Region).Include(ph => ph.SchoolPhones).FirstOrDefaultAsync(s => s.Id == schoolId);

        }


        public async Task<SchoolForUpdate?> GetSchoolByIdForUpdateAsync(Guid schoolId)
        {
            return await dbContext.Schools
       .Include(s => s.City)
       .Include(s => s.Region)
       .Include(s => s.SchoolPhones)
       .Where(s => s.Id == schoolId)
       .Select(s => new SchoolForUpdate
       {
           Id = s.Id,
           NameAr = s.NameAr,
           NameEn = s.NameEn,
           AddressAr = s.AddressAr,
           AddressEn = s.AddressEn,
           PostalCode = s.PostalCode,
           MainPhone = s.MainPhone,
           Email = s.Email,
           SchoolType = (int)s.SchoolType,
           GenderType = (int)s.GenderType,
           CurriculumType = (int)s.CurriculumType,
           SchoolLevel = (int)s.SchoolLevel,
           CityId = s.CityId,
           CityName = s.City.NameEn,
           RegionId = s.RegionId,
           RegionName = s.Region.NameEn,
           PhoneNumberList = s.SchoolPhones.Select(p => p.PhoneNumber).ToList()
       })
       .FirstOrDefaultAsync();

        }
        public IQueryable<School> GetSchoolsWithCityAndRegionQueryable()
        {
            return GetTableNoTracking()
         .Include(s => s.City)
         .Include(s => s.Region);

        }
    }
}
