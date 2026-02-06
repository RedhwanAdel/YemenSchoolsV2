using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Marks.Queries.GetSectionMarkReport
{
    public class GetSectionMarkReportQueryHandler : ResponseHandler, IRequestHandler<GetSectionMarkReportQuery, Response<SectionMarkReportDto>>
    {
        private readonly IMarkRepository _markRepository;
        private readonly ISectionSubjectRepository _sectionSubjectRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public GetSectionMarkReportQueryHandler(
            IMarkRepository markRepository,
            ISectionSubjectRepository sectionSubjectRepository,
            IStudentRepository studentRepository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _markRepository = markRepository;
            _sectionSubjectRepository = sectionSubjectRepository;
            _studentRepository = studentRepository;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<SectionMarkReportDto>> Handle(GetSectionMarkReportQuery request, CancellationToken cancellationToken)
        {
            var sectionSubject = await _sectionSubjectRepository.GetSectionSubjectsInfoAsync(request.SectionSubjectId);
            if (sectionSubject == null)
            {
                return NotFound<SectionMarkReportDto>("SectionSubject not found.");
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

            return Success(reportDto);
        }
    }
}
