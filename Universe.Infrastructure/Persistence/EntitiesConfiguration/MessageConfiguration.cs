using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universe.Core.Entities;

namespace Universe.Infrastructure.Persistence.Configurations;

internal sealed class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Subject)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Body)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(x => x.IsRead)
            .HasDefaultValue(false);

        builder.Property(x => x.IsDeletedBySender)
            .HasDefaultValue(false);

        builder.Property(x => x.IsDeletedByReceiver)
            .HasDefaultValue(false);

        builder.HasIndex(x => x.SenderId);

        builder.HasIndex(x => x.ReceiverId);

        builder.HasIndex(x => new { x.ReceiverId, x.IsRead });

        builder.HasIndex(x => x.ParentMessageId);

        builder.HasOne(x => x.Sender)
            .WithMany()
            .HasForeignKey(x => x.SenderId);

        builder.HasOne(x => x.Receiver)
            .WithMany()
            .HasForeignKey(x => x.ReceiverId);

        builder.HasOne(x => x.ParentMessage)
            .WithMany(x => x.Replies)
            .HasForeignKey(x => x.ParentMessageId);
    }
}