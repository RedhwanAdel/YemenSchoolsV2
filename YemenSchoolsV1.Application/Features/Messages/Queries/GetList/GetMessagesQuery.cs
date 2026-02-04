using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto.Messages;
using YemenSchoolsV1.Application.Wrappers;

namespace YemenSchoolsV1.Application.Features.Messages.Queries.GetList
{
    public class GetMessagesQuery : IRequest<PaginatedResponse<MessageDto>>
    {
        public MessageParams MessageParams { get; set; }
        public GetMessagesQuery(MessageParams messageParams) => MessageParams = messageParams;
    }
}
