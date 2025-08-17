using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
    public class MarkRepository : GenericRepositoryAsync<Mark>, IMarkRepository
    {
        private readonly YemenShoolsDbContext _context;

        public MarkRepository(YemenShoolsDbContext context) : base(context)
        {
            _context = context;
        }

        // إضافة مجموعة من الدرجات
        public async Task AddMarksAsync(IEnumerable<Mark> marks)
        {
            await _context.Marks.AddRangeAsync(marks);
            await _context.SaveChangesAsync();
        }


        // جلب درجة حسب المعرف
        public async Task<Mark?> GetMarkByIdAsync(Guid markId)
        {
            return await _context.Marks
                                 .Include(m => m.Student)
                                 .Include(m => m.SectionSubject)
                                 .FirstOrDefaultAsync(m => m.Id == markId);
        }

        // جلب درجات طالب معين
        public async Task<IEnumerable<Mark>> GetMarksByStudentIdAsync(Guid studentId)
        {
            return await _context.Marks
                                 .Include(m => m.SectionSubject)
                                     .ThenInclude(ss => ss.GradeSubject)
                                         .ThenInclude(gs => gs.Subject) // للوصول إلى اسم المادة
                                 .Where(m => m.StudentId == studentId)
                                 .ToListAsync();
        }

        // جلب درجات مادة في شعبة
        public async Task<IEnumerable<Mark>> GetMarksBySectionSubjectAsync(Guid sectionSubjectId)
        {
            return await _context.Marks
                                 .Include(m => m.Student)
                                 .Where(m => m.SectionSubjectId == sectionSubjectId)
                                 .ToListAsync();
        }



    }
}
