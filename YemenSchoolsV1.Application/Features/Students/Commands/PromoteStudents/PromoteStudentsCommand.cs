using MediatR;
using YemenSchoolsV1.Application.Bases;

namespace YemenSchoolsV1.Application.Features.Students.Commands.PromoteStudents
{
    public class PromoteStudentsCommand : IRequest<Response<string>>
    {
        public List<Guid> StudentIds { get; set; } = new();
        public Guid NewSectionId { get; set; }
    }
}
