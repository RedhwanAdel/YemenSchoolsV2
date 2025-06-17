using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Domain.Enums;
using YemenSchoolsV1.Persistence.Repositories;

namespace YemenSchoolsV1.Services.Implementations
{
    public class SchoolService : ISchoolService
    {
        #region filed
        private readonly ISchoolRepositry schoolRepositry;

        #endregion

        #region constractor
        public SchoolService(ISchoolRepositry schoolRepositry)
        {
            this.schoolRepositry = schoolRepositry;
        }
        #endregion

        #region handel acrtions



        public async Task<List<School>> GetAllSchoolsAsync()
        {
            return await schoolRepositry.GetAllAsync();
        }

        public async Task<School?> GetSchoolDetailsAsync(Guid id)
        {
            return await schoolRepositry.GetByIdAsync(id);
        }
        public async Task<School?> GetSchoolDetailsIncludeAsync(Guid id)
        {
            return await schoolRepositry.GetSchoolDetailsInculdeAsync(id);
        }
        public async Task<School?> CreateSchoolAsync(School school)
        {
            if (school == null)
            {
                throw new ArgumentNullException(nameof(school));
            }
            return await schoolRepositry.AddAsync(school);
        }

        public async Task<School?> EditSchoolAsync(Guid id, School school)
        {
            if (school == null)
            {
                throw new ArgumentNullException(nameof(school));
            }
            var existingSchool = await schoolRepositry.GetByIdAsync(id);
            if (existingSchool == null) { return null; }
            return await schoolRepositry.UpdateAsync(id, school);
        }

        public async Task<bool> DeleteSchoolAsync(Guid id)
        {
            var school = await schoolRepositry.GetByIdAsync(id);
            if (school == null)
                return false;
            return await schoolRepositry.DeleteAsync(id);
        }

        public IQueryable<School> FilterSchoolPaginatedQuerable(SchoolOrdering orderingEnum, string search, Guid? cityId, Guid? regionId)
        {
            var queryable = schoolRepositry.GetSchoolsWithCityAndRegionQueryable();

            // فلترة البحث


            if (cityId != Guid.Empty && cityId != null)
            {
                queryable = queryable.Where(x => x.CityId == cityId);
            }
            if (regionId != Guid.Empty && regionId != null)
            {
                queryable = queryable.Where(x => x.RegionId == regionId);
            }
            if (!string.IsNullOrEmpty(search))
            {
                queryable = queryable.Where(x => x.NameAr.ToLower().Contains(search.ToLower()));
            }

            // الترتيب
            queryable = orderingEnum switch
            {
                SchoolOrdering.Name => queryable.OrderBy(x => x.NameAr),
                SchoolOrdering.city => queryable.OrderBy(x => x.City.NameAr),
                SchoolOrdering.region => queryable.OrderBy(x => x.Region.NameAr),
                _ => queryable.OrderBy(x => x.NameAr) // في حالة لم يتم تحديد ترتيب
            };

            // إرجاع الاستعلام
            return queryable;

        }

        public async Task CreateSchoolPhonsRangAsync(List<SchoolPhone> schoolPhones)
        {
           await schoolRepositry.CreateSchoolPhonesRangAsync(schoolPhones);
        }



        #endregion
    }
}
