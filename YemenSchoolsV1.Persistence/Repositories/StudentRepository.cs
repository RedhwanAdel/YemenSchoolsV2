using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Features.Students.Queries.GetStudentsBySchoolId;
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

        public async Task<Student?> GetStudentWithDetailsAsync(Guid studentId)
        {
            return await _context.Students
                .Include(s => s.School)
                .Include(s => s.CurrentSection)
                    .ThenInclude(sec => sec.SchoolGrade)
                        .ThenInclude(sg => sg.StageGrade)
                            .ThenInclude(sg => sg.Grade)
                .Include(s => s.CurrentSection)
                    .ThenInclude(sec => sec.SchoolGrade)
                        .ThenInclude(sg => sg.StageGrade)
                            .ThenInclude(sg => sg.Stage)
                .Include(s => s.Marks)
                    .ThenInclude(m => m.SectionSubject)
                        .ThenInclude(ss => ss.GradeSubject)
                            .ThenInclude(gs => gs.Subject)
                .Include(s => s.AttendanceDetails)
                .FirstOrDefaultAsync(s => s.Id == studentId);
        }

        public async Task PromoteStudentsAsync(List<Guid> studentIds, Guid newAcademicYearId, Guid newSectionId)
        {
            // 1. استلام الطلاب المحددين
            var studentsToPromote = await _context.Students
                .Where(s => studentIds.Contains(s.Id))
                .ToListAsync();

            // 2. تحديث بياناتهم
            foreach (var student in studentsToPromote)
            {
                student.CurrentAcademicYearId = newAcademicYearId;
                student.CurrentSectionId = newSectionId;
            }

            // 3. حفظ التغييرات في قاعدة البيانات
            await _context.SaveChangesAsync();
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
        public async Task<IEnumerable<Student>> GetStudentsBySectionIdAsync(Guid sectionId)
        {
            return await _context.Students
                                 .Where(s => s.CurrentSectionId == sectionId)
                                 .ToListAsync();
        }
        public async Task<IEnumerable<StudentListDto>> GetStudentsBySchoolIdAsync(Guid schoolId)
        {
            return await _context.Students
                .Where(s => s.SchoolId == schoolId)
                .Include(s => s.CurrentSection)
                    .ThenInclude(sec => sec.SchoolGrade)
                        .ThenInclude(sg => sg.StageGrade)
                            .ThenInclude(stg => stg.Grade)
                .Select(s => new StudentListDto
                {
                    Id = s.Id,
                    Name = s.NameAr, // Adjust property as needed
                    RegisterNo = s.RegisterNo,
                    SectionName = s.CurrentSection != null ? s.CurrentSection.Name : null,
                    GradeName = s.CurrentSection != null &&
                                s.CurrentSection.SchoolGrade != null &&
                                s.CurrentSection.SchoolGrade.StageGrade != null &&
                                s.CurrentSection.SchoolGrade.StageGrade.Grade != null
                                ? s.CurrentSection.SchoolGrade.StageGrade.Grade.Name
                                : null
                    // Add other properties as needed
                })
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
