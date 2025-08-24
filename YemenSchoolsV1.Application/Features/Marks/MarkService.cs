using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Dto.Marks;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Marks
{
    public class MarkService : IMarkService
    {
        private readonly IMarkRepository _markRepository;
        private readonly ISectionSubjectRepository _sectionSubjectRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepositry _teacherRepository;

        public MarkService(
            IMarkRepository markRepository,
            ISectionSubjectRepository sectionSubjectRepository,
            IStudentRepository studentRepository,
            ITeacherRepositry teacherRepository
          )
        {
            _markRepository = markRepository;
            _sectionSubjectRepository = sectionSubjectRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
        }
        public async Task<IEnumerable<StudentSubjectReportDto>> GetStudentSubjectsReportAsync(Guid studentId)
        {
            return await _markRepository.GetStudentSubjectsReportAsync(studentId);
        }
        public async Task CreateMarksAsync(Guid teacherId, Guid sectionSubjectId, string assessmentType, Dictionary<Guid, double> studentScores, int maxScore)
        {
            // التحقق من صلاحية المعلم
            var sectionSubject = await _sectionSubjectRepository.GetByIdAsync(sectionSubjectId);
            if (sectionSubject == null || sectionSubject.TeacherId != teacherId)
            {
                throw new UnauthorizedAccessException("Teacher is not authorized to add marks for this subject and section.");
            }

            // التحقق من وجود درجات مسبقة
            var existingMarks = await _markRepository.GetMarksBySectionSubjectAsync(sectionSubjectId);
            if (existingMarks.Any(m => m.AssessmentType == assessmentType))
            {
                throw new InvalidOperationException("Marks for this assessment type already exist. Use the update method instead.");
            }

            // بناء قائمة الدرجات
            var marks = studentScores.Select(s => new Mark
            {
                StudentId = s.Key,
                SectionSubjectId = sectionSubjectId,
                Score = s.Value,
                AssessmentType = assessmentType,
                MaxScore = maxScore // يمكن جعلها ديناميكية
            }).ToList();

            await _markRepository.AddMarksAsync(marks);
        }

        public async Task UpdateMarksAsync(Guid teacherId, Guid sectionSubjectId, string assessmentType, Dictionary<Guid, double> studentScores)
        {
            // التحقق من صلاحية المعلم
            var sectionSubject = await _sectionSubjectRepository.GetByIdAsync(sectionSubjectId);
            if (sectionSubject == null || sectionSubject.TeacherId != teacherId)
            {
                throw new UnauthorizedAccessException("Teacher is not authorized to update marks for this subject and section.");
            }

            // جلب الدرجات الموجودة
            var existingMarks = (await _markRepository.GetMarksBySectionSubjectAsync(sectionSubjectId))
                                                      .Where(m => m.AssessmentType == assessmentType)
                                                      .ToDictionary(m => m.StudentId);

            // تحديث الدرجات
            foreach (var score in studentScores)
            {
                if (existingMarks.TryGetValue(score.Key, out var markToUpdate))
                {
                    markToUpdate.Score = score.Value;
                    await _markRepository.UpdateAsync(markToUpdate.Id, markToUpdate);
                }
            }
        }

        public async Task<StudentTranscriptDto> GetStudentTranscriptAsync(Guid studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
            {
                throw new KeyNotFoundException("Student not found.");
            }

            var marks = await _markRepository.GetMarksByStudentIdAsync(studentId);

            var transcriptDto = new StudentTranscriptDto
            {
                StudentId = student.Id,
                StudentName = student.NameAr,
                StudentSection = "Section Placeholder", // يجب جلبها من علاقة الطالب
                Marks = marks.Select(m => new MarkDto
                {
                    MarkId = m.Id,
                    SubjectName = m.SectionSubject.GradeSubject.Subject.Name,
                    AssessmentType = m.AssessmentType,
                    Score = m.Score,
                    MaxScore = m.MaxScore
                }).ToList()
            };

            transcriptDto.OverallAverage = transcriptDto.Marks.Any() ?
                                           transcriptDto.Marks.Average(m => (m.Score / m.MaxScore) * 100) : 0;

            return transcriptDto;
        }

        public async Task<SectionMarkReportDto> GetSectionMarkReportAsync(Guid sectionSubjectId)
        {
            var sectionSubject = await _sectionSubjectRepository.GetSectionSubjectsInfoAsync(sectionSubjectId);
            if (sectionSubject == null)
            {
                throw new KeyNotFoundException("SectionSubject not found.");
            }

            var marks = await _markRepository.GetMarksBySectionSubjectAsync(sectionSubjectId);
            var students = await _studentRepository.GetStudentsBySectionIdAsync(sectionSubject.SectionId);

            var reportDto = new SectionMarkReportDto
            {
                SectionId = sectionSubject.SectionId,
                SectionName = sectionSubject.Section.Name,
                SubjectId = sectionSubject.GradeSubject.Subject.Id,
                SubjectName = sectionSubject.GradeSubject.Subject.Name,
                StudentsSummary = new List<StudentPerformanceSummaryDto>()
            };

            foreach (var student in students)
            {
                var studentMarks = marks.Where(m => m.StudentId == student.Id);
                var assessmentScores = studentMarks.ToDictionary(m => m.AssessmentType, m => m.Score);
                var totalScore = studentMarks.Sum(m => m.Score);

                reportDto.StudentsSummary.Add(new StudentPerformanceSummaryDto
                {
                    StudentId = student.Id,
                    StudentName = student.NameAr,
                    AssessmentScores = assessmentScores,
                    TotalScore = totalScore
                });
            }

            return reportDto;
        }

        public async Task<IEnumerable<SectionSubjectDto>> GetTeacherSectionSubjectsAsync(Guid teacherId)
        {
            // نفترض وجود دالة في المستودع تجلب هذه البيانات
            var sectionSubjects = await _teacherRepository.GetTeacherSectionSubjectsAsync(teacherId);

            // هنا يمكنك تحويل الكيانات إلى DTOs
            var sectionSubjectDtos = sectionSubjects.Select(ss => new SectionSubjectDto
            {
                Id = ss.Id,
                SectionName = ss.Section.Name,
                SubjectName = ss.GradeSubject.Subject.Name,
                SectionId = ss.SectionId,
                GradeName = ss.Section.SchoolGrade.StageGrade.Grade.Name
            }).ToList();

            return sectionSubjectDtos;
        }
    }
}
