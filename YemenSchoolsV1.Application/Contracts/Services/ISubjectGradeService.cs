using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Services
{
    public interface ISubjectGradeService
    {
        Task<List<SubjectGrade>> GetAllSubjectGradesAsync();
        Task<SubjectGrade?> GetSubjectGradeDetailsAsync(Guid id);
        Task<SubjectGrade?> CreateSubjectGradeAsync(SubjectGrade subjectGrade);
        Task<SubjectGrade?> EditSubjectGradeAsync(Guid id, SubjectGrade subjectGrade);
        Task<bool> DeleteSubjectGradeAsync(Guid id);
    }

}
