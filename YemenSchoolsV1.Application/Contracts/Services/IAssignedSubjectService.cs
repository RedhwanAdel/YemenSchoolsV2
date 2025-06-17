using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Services
{
    public interface IAssignedSubjectService
    {
        Task<List<AssignedSubject>> GetAllAssignedSubjectsAsync();
        Task<AssignedSubject?> GetAssignedSubjectDetailsAsync(Guid id);
        Task<AssignedSubject?> AssignSubjectAsync(AssignedSubject assignedSubject);
        Task<AssignedSubject?> EditAssignedSubjectAsync(Guid id, AssignedSubject assignedSubject);
        Task<bool> DeleteAssignedSubjectAsync(Guid id);
    }

}
