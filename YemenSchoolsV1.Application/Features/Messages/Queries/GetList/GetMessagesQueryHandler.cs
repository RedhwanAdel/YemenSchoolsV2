using MediatR;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Messages;
using YemenSchoolsV1.Application.Wrappers;

namespace YemenSchoolsV1.Application.Features.Messages.Queries.GetList
{
    public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, PaginatedResponse<MessageDto>>
    {
        private readonly IMessageRepository _messageRepository;

        public GetMessagesQueryHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<PaginatedResponse<MessageDto>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            return await _messageRepository.GetMessagesForMember(request.MessageParams);
        }
    }
}
