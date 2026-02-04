using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Messages;
using YemenSchoolsV1.Application.Extensions;
using YemenSchoolsV1.Application.Wrappers;
using YemenSchoolsV1.Domain.Entities;

using YemenSchoolsV1.Application.Features.Messages.Commands.Create;
using YemenSchoolsV1.Application.Features.Messages.Commands.Delete;
using YemenSchoolsV1.Application.Features.Messages.Queries.GetList;
using YemenSchoolsV1.Application.Features.Messages.Queries.GetThread;

namespace YemenSchoolsV1.API.Controllers
{

    public class MessagesController : AppControllerBase
    {

        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            var response = await Mediator.Send(new CreateMessageCommand(createMessageDto, User.GetUserId()));
            if (response.Succeeded) return Ok(response.Data);
            return BadRequest(response.Message);
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<MessageDto>>> GetMessagesByContainer([FromQuery] MessageParams messageParams)
        {
            messageParams.MemberId = User.GetUserId();
            var response = await Mediator.Send(new GetMessagesQuery(messageParams));
            return Ok(response);
        }

        [HttpGet("thread/{recipientId}")]
        public async Task<ActionResult<IReadOnlyList<MessageDto>>> GetMessageThread(Guid recipientId)
        {
            if (recipientId == Guid.Empty) return BadRequest("Invalid recipient");
            var response = await Mediator.Send(new GetMessageThreadQuery(User.GetUserId(), recipientId));
            return Ok(response.Data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(string id)
        {
            var response = await Mediator.Send(new DeleteMessageCommand(id, User.GetUserId()));
            if (response.Succeeded) return Ok();
            return BadRequest(response.Message);
        }
    }
}
