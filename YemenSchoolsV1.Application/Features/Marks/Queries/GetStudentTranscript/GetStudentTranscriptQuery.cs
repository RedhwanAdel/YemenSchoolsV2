using MediatR;
using YemenSchoolsV1.Application.Bases;

namespace YemenSchoolsV1.Application.Features.Marks.Queries.GetStudentTranscript
{
    public class GetStudentTranscriptQuery : IRequest<Response<StudentTranscriptDto>>
    {
        public Guid StudentId { get; set; }
    }
}
