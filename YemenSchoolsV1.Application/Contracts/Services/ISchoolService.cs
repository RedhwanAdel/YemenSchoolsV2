using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Application.Contracts.Services
{
    public interface ISchoolService
    {
        public IQueryable<School> FilterSchoolPaginatedQuerable(SchoolOrdering orderingEnum, string search,Guid? cityId , Guid? regionId);

        public Task<List<School>> GetAllSchoolsAsync();
        public Task<School?> GetSchoolDetailsAsync(Guid id);
        public Task<School?> GetSchoolDetailsIncludeAsync(Guid id);
        public Task<School?> CreateSchoolAsync(School school);
        public Task<School?> EditSchoolAsync(Guid id, School school);
        public Task<bool> DeleteSchoolAsync(Guid id);


        public Task CreateSchoolPhonsRangAsync(List<SchoolPhone> schoolPhones);
    }
}
