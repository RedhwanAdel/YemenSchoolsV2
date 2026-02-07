using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Messages;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Messages.Queries.GetThread
{
    public class GetMessageThreadQueryHandler : ResponseHandler, IRequestHandler<GetMessageThreadQuery, Response<IReadOnlyList<MessageDto>>>
    {
        private readonly IMessageRepository _messageRepository;

        public GetMessageThreadQueryHandler(
            IMessageRepository messageRepository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _messageRepository = messageRepository;
        }

        public async Task<Response<IReadOnlyList<MessageDto>>> Handle(GetMessageThreadQuery request, CancellationToken cancellationToken)
        {
            var messages = await _messageRepository.GetMessageThread(request.CurrentUserId, request.RecipientId);
            return Success(messages);
        }
    }
}

