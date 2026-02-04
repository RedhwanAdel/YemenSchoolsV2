using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;

namespace YemenSchoolsV1.Application.Features.Messages.Commands.Delete
{
    public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, Response<string>>
    {
        private readonly IMessageRepository _messageRepository;

        public DeleteMessageCommandHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<Response<string>> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _messageRepository.GetMessage(request.MessageId);

            if (message == null) 
                return new Response<string>("Cannot delete this message") { Succeeded = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            if (message.SenderId != request.MemberId && message.RecipientId != request.MemberId)
                return new Response<string>("You cannot delete this message") { Succeeded = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            if (message.SenderId == request.MemberId) message.SenderDeleted = true;
            if (message.RecipientId == request.MemberId) message.RecipientDeleted = true;

            if (message.SenderDeleted && message.RecipientDeleted)
            {
                _messageRepository.DeleteMessage(message);
            }

            if (await _messageRepository.Complete()) 
                return new Response<string>("Message deleted");

            return new Response<string>("Problem deleting the message") { Succeeded = false, StatusCode = System.Net.HttpStatusCode.BadRequest };
        }
    }
}
