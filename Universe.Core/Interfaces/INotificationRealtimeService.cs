using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.Notification;

namespace Universe.Core.Interfaces;

public interface INotificationRealtimeService
{
    Task SendNotificationReceivedAsync(Guid userId, NotificationResponse notification, CancellationToken cancellationToken);
    Task SendUnreadCountChangedAsync(Guid userId, int unreadCount, CancellationToken cancellationToken);
}
