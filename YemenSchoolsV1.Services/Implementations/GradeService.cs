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
        private readonly IGradeRepositry gradeRepositry;

        public GradeService(IGradeRepositry gradeRepositry)
        {
            this.gradeRepositry = gradeRepositry;
        }
        public async Task<Grade?> CreateGradeAsync(Grade grade)
        {
            if (grade == null)
            {
                throw new ArgumentNullException(nameof(grade));
            }
            return await gradeRepositry.AddAsync(grade);
        }

        public async Task<bool> DeleteGradeAsync(Guid id)
        {
            var grade = await gradeRepositry.GetByIdAsync(id);
            if (grade == null)
                return false;
            return await gradeRepositry.DeleteAsync(id);
        }

        public async Task<Grade?> EditGradeAsync(Guid id, Grade grade)
        {
            if (grade == null)
            {
                throw new ArgumentNullException(nameof(grade));
            }
            var existingGrade = await gradeRepositry.GetByIdAsync(id);
            if (existingGrade == null) { return null; }
            return await gradeRepositry.UpdateAsync(id, grade);
        }

        public async Task<List<Grade>> GetAllGradesAsync()
        {
            return await gradeRepositry.GetAllAsync();
        }

        public async Task<Grade?> GetGradeDetailsAsync(Guid id)
        {
            return await gradeRepositry.GetByIdAsync(id);
        }
    }
}
