namespace YemenSchoolsV1.Application.Dto.Messages
{
    public class CreateMessageDto
    {
        public required Guid RecipientId { get; set; }
        public required string Content { get; set; }
    }
}
