using MediatR;
using YemenSchoolsV1.Application.Bases;

namespace YemenSchoolsV1.Application.Features.Students.Commands.RemoveParentFromStudent
{
    public class RemoveParentFromStudentCommand : IRequest<Response<string>>
    {
        public Guid StudentId { get; set; }
        public Guid ParentId { get; set; }
    }
}
