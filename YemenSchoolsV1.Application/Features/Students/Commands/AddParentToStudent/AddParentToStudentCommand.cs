using MediatR;
using YemenSchoolsV1.Application.Bases;

namespace YemenSchoolsV1.Application.Features.Students.Commands.AddParentToStudent
{
    public class AddParentToStudentCommand : IRequest<Response<string>>
    {
        public Guid StudentId { get; set; }
        public Guid ParentId { get; set; }
        public required string RelationType { get; set; }
    }
}
