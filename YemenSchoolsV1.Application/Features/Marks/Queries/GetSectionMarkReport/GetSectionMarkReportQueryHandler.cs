using MediatR;
using YemenSchoolsV1.Application.Contracts.Persistence;

namespace YemenSchoolsV1.Application.Features.Marks.Queries.GetSectionMarkReport
{
    public class GetSectionMarkReportQueryHandler : IRequestHandler<GetSectionMarkReportQuery, SectionMarkReportDto>
    {
        private readonly IMarkRepository _markRepository;
        private readonly ISectionSubjectRepository _sectionSubjectRepository;
        private readonly IStudentRepository _studentRepository;

        public GetSectionMarkReportQueryHandler(
            IMarkRepository markRepository,
            ISectionSubjectRepository sectionSubjectRepository,
            IStudentRepository studentRepository)
        {
            _markRepository = markRepository;
            _sectionSubjectRepository = sectionSubjectRepository;
            _studentRepository = studentRepository;
        }

        public async Task<SectionMarkReportDto> Handle(GetSectionMarkReportQuery request, CancellationToken cancellationToken)
        {
            var sectionSubject = await _sectionSubjectRepository.GetSectionSubjectsInfoAsync(request.SectionSubjectId);
            if (sectionSubject == null)
            {
                throw new KeyNotFoundException("SectionSubject not found.");
            }

            var marks = await _markRepository.GetMarksBySectionSubjectAsync(request.SectionSubjectId);
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
    }
}
