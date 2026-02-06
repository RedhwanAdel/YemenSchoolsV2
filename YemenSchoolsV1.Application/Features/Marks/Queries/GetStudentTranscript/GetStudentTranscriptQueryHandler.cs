using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Marks.Queries.GetStudentTranscript
{
    public class GetStudentTranscriptQueryHandler : ResponseHandler, IRequestHandler<GetStudentTranscriptQuery, Response<StudentTranscriptDto>>
    {
        private readonly IMarkRepository _markRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public GetStudentTranscriptQueryHandler(
            IMarkRepository markRepository,
            IStudentRepository studentRepository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _markRepository = markRepository;
            _studentRepository = studentRepository;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<StudentTranscriptDto>> Handle(GetStudentTranscriptQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.StudentId);
            if (student == null)
            {
                return NotFound<StudentTranscriptDto>("Student not found.");
            }

            var marks = await _markRepository.GetMarksByStudentIdAsync(request.StudentId);

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

            return Success(transcriptDto);
        }
    }
}
