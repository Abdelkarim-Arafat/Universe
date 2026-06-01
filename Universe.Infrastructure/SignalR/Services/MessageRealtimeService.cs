using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.Message;
using Universe.Core.Interfaces;
using Universe.Infrastructure.Hubs;
using Universe.Infrastructure.SignalR.Common;

namespace Universe.Infrastructure.SignalR.Services;

public class MessageRealtimeService(IHubContext<MessageHub> hubContext) : IMessageRealtimeService
{
    private readonly IHubContext<MessageHub> _hubContext = hubContext;

    public Task SendMessageReceivedAsync(Guid receiverId, MessageResponse response , CancellationToken cancellationToken)
    {
        return _hubContext.Clients
            .User(receiverId.ToString())
            .SendAsync(MessageEvents.Received, response, cancellationToken);
    }

    public Task SendMessageReplyAsync(Guid receiverId, MessageReplyResponse response, CancellationToken cancellationToken)
    {
        return _hubContext.Clients
            .User(receiverId.ToString())
            .SendAsync(MessageEvents.Replied, response, cancellationToken);
    }
}
