using MediatR;
using YemenSchoolsV1.Application.Bases;

namespace YemenSchoolsV1.Application.Features.Students.Queries.GetStudentProfileWithParents
{
    public class GetStudentProfileWithParentsQuery : IRequest<Response<StudentWithParentsDto>>
    {
        public Guid StudentId { get; set; }
    }
}
