using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Universe.Api.Extensions;
using Universe.Application.CourseOfferingServices.Commands.RemoveCourseOeffering;
using Universe.Application.TeachingSessionServices.Commands.AddSession;
using Universe.Application.TeachingSessionServices.Commands.RemoveSession;
using Universe.Application.TeachingSessionServices.Queries.GetCourseSessions;
using Universe.Application.TeachingSessionServices.SessionDtos;

namespace Universe.Api.Controllers;

[Route("teaching-sessions")]
[ApiController]
public class TeachingSessionsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("")]
    public async Task<IActionResult> AddSession(
        [FromBody] AddSessionCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpDelete("{sessionId:guid}")]
    public async Task<IActionResult> RemoveSession(
        [FromRoute] Guid sessionId,
        [FromQuery] Guid courseOfferingId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RemoveSessionCommand(sessionId , courseOfferingId), cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetCourseSessions (
        [FromQuery] Guid courseOfferingId,
        [FromQuery] int groupNumber,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCourseSessionsCommand(
            courseOfferingId , groupNumber
            ), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}