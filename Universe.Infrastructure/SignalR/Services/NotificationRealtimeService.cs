using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.Notification;
using Universe.Core.Interfaces;
using Universe.Infrastructure.Hubs;
using Universe.Infrastructure.SignalR.Common;

namespace Universe.Infrastructure.SignalR.Services;

internal class NotificationRealtimeService(IHubContext<NotificationHub> hubContext) : INotificationRealtimeService
{
    private readonly IHubContext<NotificationHub> _hubContext = hubContext;

    public Task SendNotificationReceivedAsync(Guid userId, NotificationResponse notification , CancellationToken cancellationToken)
    {
        return _hubContext.Clients
            .User(userId.ToString())
            .SendAsync(NotificationEvents.Received, notification, cancellationToken);
    }

    public Task SendUnreadCountChangedAsync(Guid userId, int unreadCount, CancellationToken cancellationToken)
    {
        return _hubContext.Clients
            .User(userId.ToString())
            .SendAsync(NotificationEvents.UnreadCountChanged, unreadCount, cancellationToken);
    }
}
