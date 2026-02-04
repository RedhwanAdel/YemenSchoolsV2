using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Parents;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Persistence
{
    public interface IParentRepository : IGenericRepositoryAsync<Parent>
    {
        Task<List<Student>> GetStudentsByParentIdAsync(Guid parentId);
        Task<Parent?> GetParentByNationalIdAsync(string nationalId);

        Task<bool> ParentExistsByNationalIdAsync(string nationalId);
        Task<Parent?> GetParentByUserIdAsync(Guid userId);
        Task<Parent?> GetParentByIdWithStudentsAsync(Guid parentId);
        Task<IEnumerable<Parent>> GetAllParentsAsync();
        Task DeactivateParentAsync(Guid parentId);
        //Task<(bool Succeeded, string Message)> DeleteParentAndRelatedDataAsync(Parent parent);
        Task AddStudentToParentAsync(ParentStudent parentStudent);
        Task RemoveStudentFromParentAsync(Guid parentId, Guid studentId);
        Task<List<TeacherInfoForParentDto>> GetTeachersForParentAsync(Guid parentId);


    }
}
