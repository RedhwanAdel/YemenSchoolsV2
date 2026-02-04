using MediatR;
using YemenSchoolsV1.Application.Contracts.Persistence;

namespace YemenSchoolsV1.Application.Features.Marks.Queries.GetStudentTranscript
{
    public class GetStudentTranscriptQueryHandler : IRequestHandler<GetStudentTranscriptQuery, StudentTranscriptDto>
    {
        private readonly IMarkRepository _markRepository;
        private readonly IStudentRepository _studentRepository;

        public GetStudentTranscriptQueryHandler(
            IMarkRepository markRepository,
            IStudentRepository studentRepository)
        {
            _markRepository = markRepository;
            _studentRepository = studentRepository;
        }

        public async Task<StudentTranscriptDto> Handle(GetStudentTranscriptQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.StudentId);
            if (student == null)
            {
                throw new KeyNotFoundException("Student not found.");
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

            return transcriptDto;
        }
    }
}
