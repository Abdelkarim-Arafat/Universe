using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Universe.Infrastructure.Hubs;

[Authorize]
public class NotificationHub : Hub
{
}
