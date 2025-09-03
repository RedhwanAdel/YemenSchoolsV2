using FinalProject.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Messages;
using YemenSchoolsV1.Application.Wrappers;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Persistence
{
    public interface IMessageRepository : IGenericRepositoryAsync<Message>
    {
        Task<bool> Complete();

        void AddMessage(Message message);
        void DeleteMessage(Message message);
        Task<Message?> GetMessage(string messageId);
        Task<PaginatedResponse<MessageDto>> GetMessagesForMember(MessageParams messageParams);
        Task<IReadOnlyList<MessageDto>> GetMessageThread(Guid currentMemberId, Guid recipientId);

        //void AddGroup(Group group);
        //Task RemoveConnection(string connectionId);
        //Task<Connection?> GetConnection(string connectionId);
        //Task<Group?> GetMessageGroup(string groupName);
        //Task<Group?> GetGroupForConnection(string connectionId);
    }
}
