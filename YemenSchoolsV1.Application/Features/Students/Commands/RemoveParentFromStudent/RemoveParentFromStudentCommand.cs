using MediatR;

namespace YemenSchoolsV1.Application.Features.Students.Commands.RemoveParentFromStudent
{
    public class RemoveParentFromStudentCommand : IRequest<(bool Succeeded, string Message)>
    {
        public Guid StudentId { get; set; }
        public Guid ParentId { get; set; }
    }
}
