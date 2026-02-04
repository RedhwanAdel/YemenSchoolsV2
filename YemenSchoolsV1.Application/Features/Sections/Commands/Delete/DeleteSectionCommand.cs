using MediatR;
using YemenSchoolsV1.Application.Bases;

namespace YemenSchoolsV1.Application.Features.Sections.Commands.Delete
{
    public class DeleteSectionCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public DeleteSectionCommand(Guid id) => Id = id;
    }
}
