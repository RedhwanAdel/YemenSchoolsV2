using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Messages;

namespace YemenSchoolsV1.Application.Features.Messages.Queries.GetThread
{
    public class GetMessageThreadQueryHandler : IRequestHandler<GetMessageThreadQuery, Response<IReadOnlyList<MessageDto>>>
    {
        private readonly IMessageRepository _messageRepository;

        public GetMessageThreadQueryHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<Response<IReadOnlyList<MessageDto>>> Handle(GetMessageThreadQuery request, CancellationToken cancellationToken)
        {
            var messages = await _messageRepository.GetMessageThread(request.CurrentUserId, request.RecipientId);
            return new Response<IReadOnlyList<MessageDto>>(messages);
        }
    }
}
