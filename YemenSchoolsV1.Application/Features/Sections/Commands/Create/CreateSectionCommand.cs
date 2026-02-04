using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.Sections.Commands.Create
{
    public class CreateSectionCommand : IRequest<Response<string>>
    {
        public CreateSectionDto Dto { get; set; }
        public CreateSectionCommand(CreateSectionDto dto) => Dto = dto;
    }
}
