using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto.Parents;

namespace YemenSchoolsV1.Application.Features.Parents.Commands.UpdateParentProfile
{
    public class UpdateParentProfileCommand : IRequest<Response<string>>
    {
        public Guid UserId { get; set; }
        public ParentUpdateDto Dto { get; set; }

        public UpdateParentProfileCommand(Guid userId, ParentUpdateDto dto)
        {
            UserId = userId;
            Dto = dto;
        }
    }
}
