using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Sections.Commands.Update
{
    public class UpdateSectionCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public UpdateSectionDto Dto { get; set; }
        public UpdateSectionCommand(Guid id, UpdateSectionDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }
}
