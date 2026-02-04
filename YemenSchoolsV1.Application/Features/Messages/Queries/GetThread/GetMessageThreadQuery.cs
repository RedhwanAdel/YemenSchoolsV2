using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto.Messages;

namespace YemenSchoolsV1.Application.Features.Messages.Queries.GetThread
{
    public class GetMessageThreadQuery : IRequest<Response<IReadOnlyList<MessageDto>>>
    {
        public Guid CurrentUserId { get; set; }
        public Guid RecipientId { get; set; }

        public GetMessageThreadQuery(Guid currentUserId, Guid recipientId)
        {
            CurrentUserId = currentUserId;
            RecipientId = recipientId;
        }
    }
}
