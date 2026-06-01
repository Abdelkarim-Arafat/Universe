using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Core.Contracts.Notification;

public record NotificationResponse(
    Guid Id,
    NotificationType Type,
    string Title,
    string Body,
    Guid? RelatedId,
    bool IsSeen,
    DateTime CreatedAt
);
