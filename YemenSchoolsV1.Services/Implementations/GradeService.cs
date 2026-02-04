using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Repositories;

namespace YemenSchoolsV1.Services.Implementations
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository gradeRepository;

        public GradeService(IGradeRepository gradeRepository)
        {
            this.gradeRepository = gradeRepository;
        }
        public async Task<Grade?> CreateGradeAsync(Grade grade)
        {
            if (grade == null)
            {
                throw new ArgumentNullException(nameof(grade));
            }
            return await gradeRepository.AddAsync(grade);
        }

        public async Task<bool> DeleteGradeAsync(Guid id)
        {
            var grade = await gradeRepository.GetByIdAsync(id);
            if (grade == null)
                return false;
            return await gradeRepository.DeleteAsync(id);
        }

        public async Task<Grade?> EditGradeAsync(Guid id, Grade grade)
        {
            if (grade == null)
            {
                throw new ArgumentNullException(nameof(grade));
            }
            var existingGrade = await gradeRepository.GetByIdAsync(id);
            if (existingGrade == null) { return null; }
            return await gradeRepository.UpdateAsync(id, grade);
        }

        public async Task<List<Grade>> GetAllGradesAsync()
        {
            return await gradeRepository.GetAllAsync();
        }

        public async Task<Grade?> GetGradeDetailsAsync(Guid id)
        {
            return await gradeRepository.GetByIdAsync(id);
        }
    }
}
