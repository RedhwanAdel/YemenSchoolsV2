using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Services.Implementations
{
    public class SubjectGradeService : ISubjectGradeService
    {
        #region Fields
        private readonly ISubjectGradeRepositry _subjectGradeRepository;
        #endregion

        #region Constructor
        public SubjectGradeService(ISubjectGradeRepositry subjectGradeRepository)
        {
            _subjectGradeRepository = subjectGradeRepository;
        }
        #endregion

        #region Handle Actions

        public async Task<List<SubjectGrade>> GetAllSubjectGradesAsync()
        {
            return await _subjectGradeRepository.GetAllAsync();
        }

        public async Task<SubjectGrade?> GetSubjectGradeDetailsAsync(Guid id)
        {
            return await _subjectGradeRepository.GetByIdAsync(id);
        }

        public async Task<SubjectGrade?> CreateSubjectGradeAsync(SubjectGrade subjectGrade)
        {
            if (subjectGrade == null)
            {
                throw new ArgumentNullException(nameof(subjectGrade));
            }
            return await _subjectGradeRepository.AddAsync(subjectGrade);
        }

        public async Task<SubjectGrade?> EditSubjectGradeAsync(Guid id, SubjectGrade subjectGrade)
        {
            if (subjectGrade == null)
            {
                throw new ArgumentNullException(nameof(subjectGrade));
            }
            var existingSubjectGrade = await _subjectGradeRepository.GetByIdAsync(id);
            if (existingSubjectGrade == null) { return null; }
            return await _subjectGradeRepository.UpdateAsync(id, subjectGrade);
        }

        public async Task<bool> DeleteSubjectGradeAsync(Guid id)
        {
            var subjectGrade = await _subjectGradeRepository.GetByIdAsync(id);
            if (subjectGrade == null)
                return false;
            return await _subjectGradeRepository.DeleteAsync(id);
        }

        #endregion
    }

}
