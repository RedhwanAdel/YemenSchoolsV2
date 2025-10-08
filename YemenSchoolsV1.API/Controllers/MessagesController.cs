using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Messages;
using YemenSchoolsV1.Application.Extensions;
using YemenSchoolsV1.Application.Wrappers;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.API.Controllers
{

    public class MessagesController(IMessageRepository messageRepository, IUserRepository userRepository, ITeacherRepositry teacherRepositry, IParentRepositry parentRepositry) : AppControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            var sender = await userRepository.GetByIdAsync(User.GetUserId());

            var recipient = await userRepository.GetByIdAsync(createMessageDto.RecipientId);

            if (recipient == null || sender == null || sender.Id == createMessageDto.RecipientId)
                throw new HubException("Cannot send message");

            var message = new Message
            {
                SenderId = sender.Id,
                RecipientId = recipient.Id,
                Content = createMessageDto.Content
            };

            var result = await messageRepository.AddAsync(message);

            if (result != null) return message.ToDto();

            return BadRequest("Failed to send message");
        }
        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<MessageDto>>> GetMessagesByContainer(
            [FromQuery] MessageParams messageParams)
        {
            messageParams.MemberId = User.GetUserId();

            return await messageRepository.GetMessagesForMember(messageParams);
        }

        [HttpGet("thread/{recipientId}")]
        public async Task<ActionResult<IReadOnlyList<MessageDto>>> GetMessageThread(Guid recipientId)
        {

            var currentUserId = User.GetUserId();



            if (recipientId == Guid.Empty)
                return BadRequest("Invalid recipient");

            var messages = await messageRepository.GetMessageThread(currentUserId, recipientId);

            return Ok(messages);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(string id)
        {
            var memberId = User.GetUserId();

            var message = await messageRepository.GetMessage(id);

            if (message == null) return BadRequest("Cannot delete this message");

            if (message.SenderId != memberId && message.RecipientId != memberId)
                return BadRequest("You cannot delete this message");

            if (message.SenderId == memberId) message.SenderDeleted = true;
            if (message.RecipientId == memberId) message.RecipientDeleted = true;

            if (message is { SenderDeleted: true, RecipientDeleted: true })
            {
                messageRepository.DeleteMessage(message);
            }

            if (await messageRepository.Complete()) return Ok();

            return BadRequest("Problem deleting the message");
        }
    }
}
