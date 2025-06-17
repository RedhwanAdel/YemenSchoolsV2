using FinalProject.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Persistence
{
    public interface IAssignedSubjectRepository : IGenericRepositoryAsync<AssignedSubject>
    {
        Task<List<AssignedSubject>> GetByTeacherIdAsync(Guid teacherId);
        Task<List<AssignedSubject>> GetBySectionIdAsync(Guid sectionId);
    }
}
