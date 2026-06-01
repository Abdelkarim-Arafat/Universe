using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Universe.Api.Extensions;
using Universe.Application.Common;
using Universe.Application.NotificationServices.Commands.DeleteNotification;
using Universe.Application.NotificationServices.Commands.MarkNotificationAsSeen;
using Universe.Application.NotificationServices.Queries.GetNotifications;
using Universe.Application.NotificationServices.Queries.GetUnreadNotificationsCount;
using Universe.Core.Constants;

namespace Universe.Api.Controllers;

[Route("notifications")]
[ApiController, Authorize]
public class NotificationController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    private Guid GetUserId() => Guid.Parse(User.GetUserId()!);

    [HttpGet]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AllRoles)]
    public async Task<IActionResult> GetNotifications([FromQuery] FilterRequest filter, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetNotificationsQuery(GetUserId(), filter), cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("unread-count")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AllRoles)]
    public async Task<IActionResult> GetUnreadNotificationsCount(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetUnreadNotificationsCountQuery(GetUserId()), cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPatch("{notificationId:guid}/seen")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AllRoles)]
    public async Task<IActionResult> MarkNotificationAsSeen([FromRoute] Guid notificationId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new MarkNotificationAsSeenCommand(notificationId, GetUserId()), cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{notificationId:guid}")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AllRoles)]
    public async Task<IActionResult> DeleteNotification([FromRoute] Guid notificationId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteNotificationCommand(notificationId, GetUserId()), cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}