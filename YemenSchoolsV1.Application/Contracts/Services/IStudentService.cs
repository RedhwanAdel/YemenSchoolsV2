using YemenSchoolsV1.Application.Dto.Students;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Services
{
    public interface IStudentService
    {
        Task<(bool Succeeded, string Message)> CreateStudentAsync(StudentCreateDto dto);
        Task<StudentWithParentsDto?> GetStudentProfileWithParentsAsync(Guid studentId);
        Task<IEnumerable<Student>> GetStudentsByAcademicYearAndSectionAsync(Guid academicYearId, Guid sectionId);
        Task<(bool Succeeded, string Message)> UpdateStudentProfileAsync(Guid studentId, StudentUpdateDto dto);
        //Task<(bool Succeeded, string Message)> DeleteStudentAsync(Guid studentId);
        Task<(bool Succeeded, string Message)> AddParentToStudentAsync(Guid studentId, Guid parentId, string relationType);
        Task<(bool Succeeded, string Message)> RemoveParentFromStudentAsync(Guid studentId, Guid parentId);


    }
}
