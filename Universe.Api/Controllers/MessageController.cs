using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Universe.Api.Extensions;
using Universe.Application.Common;
using Universe.Application.MessageServices.Commands.DeleteMessage;
using Universe.Application.MessageServices.Commands.ReadMessage;
using Universe.Application.MessageServices.Commands.ReplyMessage;
using Universe.Application.MessageServices.Commands.SendMessage;
using Universe.Application.MessageServices.Queries.GetInbox;
using Universe.Application.MessageServices.Queries.GetMessageDetails;
using Universe.Core.Constants;

namespace Universe.Api.Controllers;

[Route("messages")]
[ApiController, Authorize]
public class MessageController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    private Guid GetUserId() => Guid.Parse(User.GetUserId()!);

    [HttpPost("")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AllRoles)]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageCommand request, CancellationToken cancellationToken)
    {
        request = request with { SenderId = GetUserId() };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("{messageId:guid}/reply")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AllRoles)]
    public async Task<IActionResult> ReplyMessage(
        [FromRoute] Guid messageId,
        [FromBody] ReplyMessageCommand request,
        CancellationToken cancellationToken)
    {
        request = request with
        {
            SenderId = GetUserId(),
            ParentMessageId = messageId
        };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPatch("{messageId:guid}/read")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AllRoles)]
    public async Task<IActionResult> ReadMessage(
        [FromRoute] Guid messageId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ReadMessageCommand(messageId, GetUserId()), cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{messageId:guid}")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AllRoles)]
    public async Task<IActionResult> DeleteMessage([FromRoute] Guid messageId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteMessageCommand(messageId, GetUserId()), cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpGet]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AllRoles)]
    public async Task<IActionResult> GetInbox([FromQuery] FilterRequest filter, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetInboxQuery(GetUserId(), filter), cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{messageId:guid}")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AllRoles)]
    public async Task<IActionResult> GetMessage([FromRoute] Guid messageId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetMessageQuery(GetUserId(), messageId), cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}