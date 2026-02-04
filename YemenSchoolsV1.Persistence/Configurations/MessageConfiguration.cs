using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Content)
                .IsRequired();

            builder.Property(m => m.MessageSent)
                .HasDefaultValueSql("GETUTCDATE()");

            // Sender relationship
            builder.HasOne(m => m.Sender)
                .WithMany(u => u.MessagesSent)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Recipient relationship
            builder.HasOne(m => m.Recipient)
                .WithMany(u => u.MessagesReceived)
                .HasForeignKey(m => m.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes for performance
            builder.HasIndex(m => m.SenderId);
            builder.HasIndex(m => m.RecipientId);
            builder.HasIndex(m => m.MessageSent);
        }
    }
}
