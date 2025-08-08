using FinalProject.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Persistence
{
    public interface IStudentRepository : IGenericRepositoryAsync<Student>
    {
        Task<bool> StudentExistsByRegisterNoAsync(string registerNo);
        //Task<Student?> GetStudentByIdAsync(Guid studentId);
        Task<Student?> GetStudentByIdWithParentsAsync(Guid studentId);
        //Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<IEnumerable<Student>> GetStudentsByAcademicYearAndSectionAsync(Guid academicYearId, Guid sectionId);
        //Task AddStudentAsync(Student student);
        //Task UpdateStudentAsync(Student student);
        //Task<(bool Succeeded, string Message)> DeleteStudentAsync(Guid studentId);
        Task AddParentToStudentAsync(ParentStudent parentStudent);
        Task RemoveParentFromStudentAsync(Guid studentId, Guid parentId);
    }
}
