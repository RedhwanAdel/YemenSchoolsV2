using YemenSchoolsV1.Application.Dto.Parents;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Services
{
    public interface IParentService
    {
        Task<(bool Succeeded, string Message)> CreateParentWithUserAsync(ParentCreateDto dto, string defaultPassword);

        // R - Read
        Task<ParentWithStudentsDto?> GetParentWithStudentsAsync(Guid parentId);
        Task<ParentWithStudentsDto?> GetParentProfileAsync(Guid userId);
        Task<IEnumerable<Parent>> GetAllParentsAsync();

        // U - Update
        Task<(bool Succeeded, string Message)> UpdateParentProfileAsync(Guid userId, ParentUpdateDto dto);

        // D - Delete/Deactivate
        Task<(bool Succeeded, string Message)> DeactivateParentAsync(Guid parentId);
        //Task<(bool Succeeded, string Message)> DeleteParentAsync(Guid parentId);

        // Parent-Student Relationships
        Task<(bool Succeeded, string Message)> AddStudentToParentAsync(Guid parentId, Guid studentId, string relationType);
        Task<(bool Succeeded, string Message)> RemoveStudentFromParentAsync(Guid parentId, Guid studentId);

    }
}
