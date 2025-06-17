using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Services
{
    public interface IGradeService
    {
        public Task<List<Grade>> GetAllGradesAsync();
        public Task<Grade?> GetGradeDetailsAsync(Guid id);
        public Task<Grade?> CreateGradeAsync(Grade grade );
        public Task<Grade?> EditGradeAsync(Guid id, Grade grade);
        public Task<bool> DeleteGradeAsync(Guid id);
    }
}
