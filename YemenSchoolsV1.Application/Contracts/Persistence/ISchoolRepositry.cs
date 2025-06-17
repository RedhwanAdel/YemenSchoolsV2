using FinalProject.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Persistence
{
    public interface ISchoolRepositry : IGenericRepositoryAsync<School>
    {
        public IQueryable<School> GetSchoolsWithCityAndRegionQueryable();
        Task<School?> GetSchoolDetailsInculdeAsync(Guid cityId);
        Task CreateSchoolPhonesRangAsync(List<SchoolPhone> schoolPhones);

    }
}
