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
    public class AssignedSubjectService : IAssignedSubjectService
    {
        #region Fields
        private readonly IAssignedSubjectRepository _assignedSubjectRepository;
        #endregion

        #region Constructor
        public AssignedSubjectService(IAssignedSubjectRepository assignedSubjectRepository)
        {
            _assignedSubjectRepository = assignedSubjectRepository;
        }
        #endregion

        #region Handle Actions

        public async Task<List<AssignedSubject>> GetAllAssignedSubjectsAsync()
        {
            return await _assignedSubjectRepository.GetAllAsync();
        }

        public async Task<AssignedSubject?> GetAssignedSubjectDetailsAsync(Guid id)
        {
            return await _assignedSubjectRepository.GetByIdAsync(id);
        }

        public async Task<AssignedSubject?> AssignSubjectAsync(AssignedSubject assignedSubject)
        {
            if (assignedSubject == null)
            {
                throw new ArgumentNullException(nameof(assignedSubject));
            }
            return await _assignedSubjectRepository.AddAsync(assignedSubject);
        }

        public async Task<AssignedSubject?> EditAssignedSubjectAsync(Guid id, AssignedSubject assignedSubject)
        {
            if (assignedSubject == null)
            {
                throw new ArgumentNullException(nameof(assignedSubject));
            }
            var existingAssignedSubject = await _assignedSubjectRepository.GetByIdAsync(id);
            if (existingAssignedSubject == null) { return null; }
            return await _assignedSubjectRepository.UpdateAsync(id, assignedSubject);
        }

        public async Task<bool> DeleteAssignedSubjectAsync(Guid id)
        {
            var assignedSubject = await _assignedSubjectRepository.GetByIdAsync(id);
            if (assignedSubject == null)
                return false;
            return await _assignedSubjectRepository.DeleteAsync(id);
        }

        #endregion
    }

}
