namespace YemenSchoolsV1.Application.Dto.Messages
{

    public class MessageDto
    {
        public required Guid Id { get; set; }
        public required Guid SenderId { get; set; }
        public required string SenderDisplayName { get; set; }
        public string? SenderImageUrl { get; set; }
        public required Guid RecipientId { get; set; }
        public required string RecipientDisplayName { get; set; }
        public string? RecipientImageUrl { get; set; }
        public required string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; }
    }
}
