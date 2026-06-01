using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Contracts.Message;

public sealed record MessageReplyResponse(
    Guid Id,
    Guid SenderId,
    Guid ReceiverId,
    Guid? ParentMessageId,
    string SenderName,
    string Body,
    bool IsRead,
    DateTime CreatedAt
);