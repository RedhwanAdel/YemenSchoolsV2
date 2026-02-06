using MediatR;
using YemenSchoolsV1.Application.Bases;

namespace YemenSchoolsV1.Application.Features.Students.Queries.GetStudentsBySchoolId
{
    public class GetStudentsBySchoolIdQuery : IRequest<Response<IEnumerable<StudentListDto>>>
    {
        public Guid SchoolId { get; set; }
    }
}
