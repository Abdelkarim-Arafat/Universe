using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Infrastructure.SignalR.Common;

public static class NotificationEvents
{
    public const string Received = "NotificationReceived";
    public const string Seen = "NotificationSeen";
    public const string UnreadCountChanged = "NotificationUnreadCountChanged";
}