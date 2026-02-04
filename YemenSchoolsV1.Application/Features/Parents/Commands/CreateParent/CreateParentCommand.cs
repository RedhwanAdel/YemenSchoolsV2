using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto.Parents;

namespace YemenSchoolsV1.Application.Features.Parents.Commands.CreateParent
{
    public class CreateParentCommand : IRequest<Response<object>>
    {
        public ParentCreateDto Dto { get; set; }

        public CreateParentCommand(ParentCreateDto dto)
        {
            Dto = dto;
        }
    }
}
