using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
    public class StudentRepository : GenericRepositoryAsync<Student>, IStudentRepository
    {
        private readonly YemenShoolsDbContext _context;

        public StudentRepository(YemenShoolsDbContext _context) : base(_context)
        {
            this._context = _context;
        }
        public async Task<bool> StudentExistsByRegisterNoAsync(string registerNo) =>
            await _context.Students.AnyAsync(s => s.RegisterNo == registerNo);


        public async Task<Student?> GetStudentByIdWithParentsAsync(Guid studentId)
        {
            return await _context.Students
                                 .Include(s => s.Parents)
                                     .ThenInclude(ps => ps.Parent)
                                 .FirstOrDefaultAsync(s => s.Id == studentId);
        }
        public async Task<IEnumerable<Student>> GetStudentsByAcademicYearAndSectionAsync(Guid academicYearId, Guid sectionId)
        {
            return await _context.Students
                                 .Where(s => s.CurrentAcademicYearId == academicYearId && s.CurrentSectionId == sectionId)
                                 .ToListAsync();
        }

        public async Task AddParentToStudentAsync(ParentStudent parentStudent)
        {
            await _context.ParentStudents.AddAsync(parentStudent);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// تزيل علاقة ولي أمر من طالب.
        /// </summary>
        public async Task RemoveParentFromStudentAsync(Guid studentId, Guid parentId)
        {
            var parentStudent = await _context.ParentStudents
                                              .FirstOrDefaultAsync(ps => ps.StudentId == studentId && ps.ParentId == parentId);
            if (parentStudent != null)
            {
                _context.ParentStudents.Remove(parentStudent);
                await _context.SaveChangesAsync();
            }
        }

    }
}
