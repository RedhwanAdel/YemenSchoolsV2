using MediatR;

namespace YemenSchoolsV1.Application.Features.Students.Queries.GetStudentProfileWithParents
{
    public class GetStudentProfileWithParentsQuery : IRequest<StudentWithParentsDto>
    {
        public Guid StudentId { get; set; }
    }
}
