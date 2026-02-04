using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Messages;
using YemenSchoolsV1.Application.Extensions;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.API.SignalR
{

    [Authorize]
    public class MessageHub(IMessageRepository messageRepository
         , IHubContext<PresenceHub> presenceHub, IUserRepository userRepository, ITeacherRepository teacherRepository, IParentRepository parentRepository) : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var otherUserId = httpContext?.Request?.Query["userId"].ToString()
     ?? throw new HubException("Other user not found");


            // الآن استخدم recipient.Id لإنشاء المجموعة وجلب الرسائل
            var groupName = GetGroupName(GetUserId(), otherUserId);

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            var messages = await messageRepository.GetMessageThread(Guid.Parse(GetUserId()), Guid.Parse(otherUserId));

            await Clients.Group(groupName).SendAsync("ReceiveMessageThread", messages);

        }

        public async Task SendMessage(CreateMessageDto createMessageDto)
        {
            var sender = await userRepository.GetByIdAsync(Guid.Parse(GetUserId()));
            var recipient = await userRepository.GetByIdAsync(createMessageDto.RecipientId);

            if (recipient == null || sender == null || sender.Id == createMessageDto.RecipientId)
                throw new HubException("Cannot send message");


            var message = new Message
            {
                SenderId = sender.Id,
                RecipientId = recipient.Id,
                Content = createMessageDto.Content
            };

            //var groupName = GetGroupName(sender.Id.ToString(), recipient.Id.ToString());
            //var group = await messageRepository.GetMessageGroup(groupName);
            //var userInGroup = group != null && group.Connections.Any(x =>
            //     x.UserId == message.RecipientId);

            //if (userInGroup)
            //{
            //    message.DateRead = DateTime.UtcNow;
            //}
            var result = await messageRepository.AddAsync(message);

            if (result != null)
            {
                var group = GetGroupName(sender.Id.ToString(), recipient.Id.ToString());
                await Clients.Group(group).SendAsync("NewMessage", message.ToDto());
                //var connections = await PresenceTracker.GetConnectionsForUser(recipient.Id.ToString());
                //if (connections != null && connections.Count > 0 && !userInGroup)
                //{
                //    await presenceHub.Clients.Clients(connections)
                //        .SendAsync("NewMessageReceived", message.ToDto());
                //}
            }

        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            //await messageRepository.RemoveConnection(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        //private async Task<bool> AddToGroup(string groupName)
        //{
        //    var group = await messageRepository.GetMessageGroup(groupName);
        //    var connection = new Connection(Context.ConnectionId, GetUserId());

        //    if (group == null)
        //    {
        //        group = new Group(groupName);
        //        messageRepository.AddGroup(group);
        //    }

        //    group.Connections.Add(connection);

        //    return await messageRepository.SaveAllAsync();
        //}

        private static string GetGroupName(string? caller, string? other)
        {
            var stringCompare = string.CompareOrdinal(caller, other) < 0;
            return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
        }

        private string GetUserId()
        {
            return Context.User.GetUserId().ToString();
        }
    }
}
