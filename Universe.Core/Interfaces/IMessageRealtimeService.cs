using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.Message;

namespace Universe.Core.Interfaces;

public interface IMessageRealtimeService
{
    Task SendMessageReceivedAsync(Guid receiverId, MessageResponse response, CancellationToken cancellationToken);

    Task SendMessageReplyAsync(Guid receiverId, MessageReplyResponse response, CancellationToken cancellationToken);
}
