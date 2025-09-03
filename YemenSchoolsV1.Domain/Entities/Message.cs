namespace YemenSchoolsV1.Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public required string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; } = DateTime.UtcNow;
        public bool SenderDeleted { get; set; }
        public bool RecipientDeleted { get; set; }

        // nav properties
        public required Guid SenderId { get; set; }
        public AppUser Sender { get; set; } = null!;
        public required Guid RecipientId { get; set; }
        public AppUser Recipient { get; set; } = null!;
    }
}
