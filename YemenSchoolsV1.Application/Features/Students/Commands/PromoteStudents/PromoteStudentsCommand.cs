using MediatR;
using YemenSchoolsV1.Application.Features.Students.Commands.PromoteStudents;

namespace YemenSchoolsV1.Application.Features.Students.Commands.PromoteStudents
{
    public class PromoteStudentsCommand : IRequest<(bool Succeeded, string Message)>
    {
        public List<Guid> StudentIds { get; set; } = new();
        public Guid NewSectionId { get; set; }
    }
}
