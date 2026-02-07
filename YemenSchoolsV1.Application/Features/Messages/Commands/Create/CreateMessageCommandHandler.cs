using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Messages;
using YemenSchoolsV1.Application.Extensions;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;


namespace YemenSchoolsV1.Application.Features.Messages.Commands.Create
{
    public class CreateMessageCommandHandler : ResponseHandler, IRequestHandler<CreateMessageCommand, Response<MessageDto>>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;

        public CreateMessageCommandHandler(
            IMessageRepository messageRepository,
            IUserRepository userRepository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        public async Task<Response<MessageDto>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var sender = await _userRepository.GetByIdAsync(request.SenderId);
            var recipient = await _userRepository.GetByIdAsync(request.CreateMessageDto.RecipientId);

            if (recipient == null || sender == null || sender.Id == request.CreateMessageDto.RecipientId)
                return BadRequest<MessageDto>("Cannot send message");

            var message = new Message
            {
                SenderId = sender.Id,
                RecipientId = recipient.Id,
                Content = request.CreateMessageDto.Content
            };

            var result = await _messageRepository.AddAsync(message);

            if (result != null)
                return Success(message.ToDto());

            return BadRequest<MessageDto>("Failed to send message");
        }
    }
}
