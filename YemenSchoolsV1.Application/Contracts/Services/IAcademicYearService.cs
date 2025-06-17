using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Services
{
    public interface IAcademicYearService
    {
        public Task<List<AcademicYear>> GetAllYearsAsync();
        public Task<AcademicYear?> GetYearDetailsAsync(Guid id);
        public Task<AcademicYear?> CreateYearAsync(AcademicYear year);
        public Task<AcademicYear?> EditYearAsync(Guid id, AcademicYear year);
        public Task<bool> DeleteYearAsync(Guid id);
    }
}
