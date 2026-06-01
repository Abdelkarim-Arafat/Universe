using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Contracts.Message;

public record MessageResponse(
    Guid Id,
    string Subject,
    string Body,
    Guid SenderId,
    string SenderName,
    DateTime CreatedAt,
    bool IsRead
);
