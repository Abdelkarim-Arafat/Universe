using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;

public class Message : BaseEntity
{
    public Guid Id { get; set; }
    public Message() { Id = Guid.CreateVersion7(); }
    public Guid SenderId { get; set; }
    public ApplicationUser Sender { get; set; } = default!;
    public Guid ReceiverId { get; set; }
    public ApplicationUser Receiver { get; set; } = default!;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;

    // Soft delete per user
    public bool IsDeletedBySender { get; set; } = false;
    public bool IsDeletedByReceiver { get; set; } = false;

    public Guid? ParentMessageId { get; set; }
    public Message? ParentMessage { get; set; }
    public ICollection<Message> Replies { get; set; } = [];
}