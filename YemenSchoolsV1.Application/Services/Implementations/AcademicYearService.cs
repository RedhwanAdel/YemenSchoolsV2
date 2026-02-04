using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Services.Implementations
{
    public class AcademicYearService : IAcademicYearService
    {
        private readonly IAcademicYearRepository academicYearRepository;

        public AcademicYearService(IAcademicYearRepository academicYearRepository)
        {
            this.academicYearRepository = academicYearRepository;
        }
        public async Task<AcademicYear?> CreateYearAsync(AcademicYear year)
        {

            var existingYears = await academicYearRepository.GetYearsBySchoolIdAsync(year.SchoolId);


            if (existingYears == null || existingYears.Count == 0)
            {
                year.IsCurrentYear = true;
            }
            else
            {
                year.IsCurrentYear = false;
            }



            if (year == null)
            {
                throw new ArgumentNullException(nameof(year));
            }
            return await academicYearRepository.AddAsync(year);
        }

        public async Task<bool> DeleteYearAsync(Guid id)
        {
            var city = await academicYearRepository.GetByIdAsync(id);
            if (city == null)
                return false;
            return await academicYearRepository.DeleteAsync(id);
        }

        public async Task<AcademicYear?> EditYearAsync(Guid id, AcademicYear year)
        {
            if (year == null)
            {
                throw new ArgumentNullException(nameof(year));
            }
            var existingYear = await academicYearRepository.GetByIdAsync(id);
            if (existingYear == null) { return null; }
            return await academicYearRepository.UpdateAsync(id, year);
        }

        public async Task<List<AcademicYear>> GetAllYearsAsync()
        {
            return await academicYearRepository.GetAllAsync();
        }

        public async Task<AcademicYear?> GetYearDetailsAsync(Guid id)
        {
            return await academicYearRepository.GetByIdAsync(id);
        }
    }
}
