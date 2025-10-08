using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Messages;
using YemenSchoolsV1.Application.Extensions;
using YemenSchoolsV1.Application.Wrappers;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
    public class MessageRepository : GenericRepositoryAsync<Message>, IMessageRepository
    {
        private readonly YemenShoolsDbContext context;

        public MessageRepository(YemenShoolsDbContext context) : base(context)
        {
            this.context = context;
        }

        //public void AddGroup(Group group)
        //{
        //    context.Groups.Add(group);
        //}

        public void AddMessage(Message message)
        {
            context.Messages.Add(message);
        }
        public async Task<bool> Complete()
        {
            try
            {
                return await context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occured while saving changes", ex);
            }
        }
        public void DeleteMessage(Message message)
        {
            context.Messages.Remove(message);
        }

        //public async Task<Connection?> GetConnection(string connectionId)
        //{
        //    return await context.Connections.FindAsync(connectionId);
        //}

        //public async Task<Group?> GetGroupForConnection(string connectionId)
        //{
        //    return await context.Groups
        //        .Include(x => x.Connections)
        //        .Where(x => x.Connections.Any(c => c.ConnectionId == connectionId))
        //        .FirstOrDefaultAsync();
        //}

        public async Task<Message?> GetMessage(string messageId)
        {
            return await context.Messages.FindAsync(messageId);
        }

        //public async Task<Group?> GetMessageGroup(string groupName)
        //{
        //    return await context.Groups
        //        .Include(x => x.Connections)
        //        .FirstOrDefaultAsync(x => x.Name == groupName);
        //}

        //public async Task<PaginatedResponse<MessageDto>> GetMessagesForMember(MessageParams messageParams)
        //{
        //    var query = context.Messages
        //        .OrderByDescending(x => x.MessageSent)
        //        .AsQueryable();

        //    query = messageParams.Container switch
        //    {
        //        "Outbox" => query.Where(x =>
        //            x.SenderId == messageParams.MemberId &&
        //            x.SenderDeleted == false),

        //        _ => query.Where(x =>
        //            x.RecipientId == messageParams.MemberId &&
        //            x.RecipientDeleted == false)
        //    };

        //    var messageQuery = query.Select(MessageExtensions.ToDtoProjection());

        //    return await messageQuery.ToPaginatedListAsync(
        //        messageParams.PageNumber,
        //        messageParams.PageSize
        //    );
        //}

        public async Task<PaginatedResponse<MessageDto>> GetMessagesForMember(MessageParams messageParams)
        {
            IQueryable<Message> query = messageParams.Container switch
            {
                "Outbox" => context.Messages.Where(m => m.SenderId == messageParams.MemberId && !m.SenderDeleted),
                _ => context.Messages.Where(m => m.RecipientId == messageParams.MemberId && !m.RecipientDeleted)
            };

            // الخطوة 1: البحث عن معرفات أحدث الرسائل لكل طرف آخر
            var latestMessageIdsQuery = query
                .GroupBy(m => messageParams.Container == "Outbox" ? m.RecipientId : m.SenderId)
                .Select(g => g.Max(m => m.Id));

            // الخطوة 2: جلب الرسائل الفعلية
            var finalQuery = context.Messages
                .Where(m => latestMessageIdsQuery.Contains(m.Id))
                .OrderByDescending(m => m.MessageSent)
                .Select(MessageExtensions.ToDtoProjection());

            // الخطوة 3: تطبيق Pagination
            return await finalQuery.ToPaginatedListAsync(
                messageParams.PageNumber,
                messageParams.PageSize
            );
        }


        public async Task<IReadOnlyList<MessageDto>> GetMessageThread(Guid currentMemberId, Guid recipientId)
        {
            await context.Messages
                .Where(x => x.RecipientId == currentMemberId
                    && x.SenderId == recipientId && x.DateRead == null)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(x => x.DateRead, DateTime.UtcNow));

            return await context.Messages
                .Where(x => (x.RecipientId == currentMemberId && x.RecipientDeleted == false
                    && x.SenderId == recipientId)
                    || (x.SenderId == currentMemberId
                    && x.SenderDeleted == false && x.RecipientId == recipientId))
                .OrderBy(x => x.MessageSent)
                .Select(MessageExtensions.ToDtoProjection())
                .ToListAsync();
        }

        //public async Task RemoveConnection(string connectionId)
        //{
        //    await context.Connections
        //        .Where(x => x.ConnectionId == connectionId)
        //        .ExecuteDeleteAsync();
        //}
    }
}
