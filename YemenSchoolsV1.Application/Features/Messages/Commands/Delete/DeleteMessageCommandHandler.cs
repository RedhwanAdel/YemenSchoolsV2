using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Messages.Commands.Delete
{
    public class DeleteMessageCommandHandler : ResponseHandler, IRequestHandler<DeleteMessageCommand, Response<string>>
    {
        private readonly IMessageRepository _messageRepository;

        public DeleteMessageCommandHandler(
            IMessageRepository messageRepository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _messageRepository = messageRepository;
        }

        public async Task<Response<string>> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _messageRepository.GetMessage(request.MessageId);

            if (message == null) 
                return BadRequest<string>("Cannot delete this message");

            if (message.SenderId != request.MemberId && message.RecipientId != request.MemberId)
                return BadRequest<string>("You cannot delete this message");

            if (message.SenderId == request.MemberId) message.SenderDeleted = true;
            if (message.RecipientId == request.MemberId) message.RecipientDeleted = true;

            if (message.SenderDeleted && message.RecipientDeleted)
            {
                _messageRepository.DeleteMessage(message);
            }

            if (await _messageRepository.Complete()) 
                return Success("Message deleted");

            return BadRequest<string>("Problem deleting the message");
        }
    }
}
