using MediatR;

namespace YemenSchoolsV1.Application.Features.Marks.Queries.GetStudentTranscript
{
    public class GetStudentTranscriptQuery : IRequest<StudentTranscriptDto>
    {
        public Guid StudentId { get; set; }
    }
}
