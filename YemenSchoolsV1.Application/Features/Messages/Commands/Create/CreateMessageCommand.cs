using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto.Messages;

namespace YemenSchoolsV1.Application.Features.Messages.Commands.Create
{
    public class CreateMessageCommand : IRequest<Response<MessageDto>>
    {
        public CreateMessageDto CreateMessageDto { get; set; }
        public Guid SenderId { get; set; }

        public CreateMessageCommand(CreateMessageDto createMessageDto, Guid senderId)
        {
            CreateMessageDto = createMessageDto;
            SenderId = senderId;
        }
    }
}
