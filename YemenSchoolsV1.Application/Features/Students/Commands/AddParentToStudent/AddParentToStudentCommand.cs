using MediatR;

namespace YemenSchoolsV1.Application.Features.Students.Commands.AddParentToStudent
{
    public class AddParentToStudentCommand : IRequest<(bool Succeeded, string Message)>
    {
        public Guid StudentId { get; set; }
        public Guid ParentId { get; set; }
        public required string RelationType { get; set; }
    }
}
