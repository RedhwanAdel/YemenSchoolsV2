using MediatR;
using YemenSchoolsV1.Application.Bases;

namespace YemenSchoolsV1.Application.Features.Messages.Commands.Delete
{
    public class DeleteMessageCommand : IRequest<Response<string>>
    {
        public string MessageId { get; set; }
        public Guid MemberId { get; set; }

        public DeleteMessageCommand(string messageId, Guid memberId)
        {
            MessageId = messageId;
            MemberId = memberId;
        }
    }
}
