using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Contracts.Message;

public sealed record MessageWithRepliesResponse(
    Guid Id,
    string Subject,
    string Body,
    Guid SenderId,
    string SenderName,
    Guid ReceiverId,
    DateTime CreatedAt,
    bool IsRead,
    IReadOnlyList<MessageReplyResponse> Replies
);


