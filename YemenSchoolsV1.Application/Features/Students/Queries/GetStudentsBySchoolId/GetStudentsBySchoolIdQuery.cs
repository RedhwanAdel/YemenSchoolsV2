using MediatR;

namespace YemenSchoolsV1.Application.Features.Students.Queries.GetStudentsBySchoolId
{
    public class GetStudentsBySchoolIdQuery : IRequest<IEnumerable<StudentListDto>>
    {
        public Guid SchoolId { get; set; }
    }
}
