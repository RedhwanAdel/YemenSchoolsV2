using YemenSchoolsV1.Application.Helpers;

namespace YemenSchoolsV1.Application.Dto.Messages
{
    public class MessageParams : PaginationParams
    {
        public Guid? MemberId { get; set; }
        public string Container { get; set; } = "Inbox";
    }
}
