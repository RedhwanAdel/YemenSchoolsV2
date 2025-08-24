using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Marks;
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

        public async Task<IEnumerable<StudentSubjectReportDto>> GetStudentSubjectsReportAsync(Guid studentId)
        {
            var marks = await _context.Marks
                .Include(m => m.SectionSubject)
                    .ThenInclude(ss => ss.GradeSubject)
                        .ThenInclude(gs => gs.Subject)
                .Where(m => m.StudentId == studentId)
                .ToListAsync();
            // 2. التحقق من وجود درجات للطالب
            if (!marks.Any())
            {
                return Enumerable.Empty<StudentSubjectReportDto>();
            }


            var subjectsReport = marks
                .GroupBy(m => m.SectionSubject.GradeSubject.Subject)
                .Select(g =>
                {
                    var subject = g.Key;
                    var grades = g.Select(m => new GradeItemDto
                    {
                        Type = m.AssessmentType ?? "غير محدد",
                        Score = m.Score,
                        Total = m.MaxScore,
                        Percentage = m.MaxScore > 0 ? $"{(m.Score / m.MaxScore * 100):F0}%" : "0%"
                    }).ToList();

                    var totalScore = g.Sum(m => m.Score);
                    var totalMax = g.Sum(m => m.MaxScore);
                    var percentage = totalMax > 0 ? (totalScore / totalMax) * 100 : 0;
                    var grade = percentage >= 90 ? "ممتاز"
                                : percentage >= 80 ? "جيد جداً"
                                : percentage >= 70 ? "جيد"
                                : percentage >= 60 ? "مقبول"
                                : "ضعيف";

                    return new StudentSubjectReportDto
                    {
                        Name = subject.Name,
                        Score = (int)percentage,
                        Grade = grade,
                        Details = new SubjectDetailsDto
                        {
                            Grades = grades
                        }
                    };
                })
                .ToList();

            return subjectsReport;
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
