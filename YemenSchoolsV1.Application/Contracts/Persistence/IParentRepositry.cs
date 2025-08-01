using FinalProject.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Persistence
{
    public interface IParentRepositry : IGenericRepositoryAsync<Parent>
    {
        Task<bool> ParentExistsByNationalIdAsync(string nationalId);
        Task<Parent?> GetParentByUserIdAsync(Guid userId);
        Task<Parent?> GetParentByIdWithStudentsAsync(Guid parentId);
        Task<IEnumerable<Parent>> GetAllParentsAsync();
        Task DeactivateParentAsync(Guid parentId);
        //Task<(bool Succeeded, string Message)> DeleteParentAndRelatedDataAsync(Parent parent);
        Task AddStudentToParentAsync(ParentStudent parentStudent);
        Task RemoveStudentFromParentAsync(Guid parentId, Guid studentId);

    }
}
