using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Universe.Api.Extensions;
using Universe.Application.TeachingSessionServices.Commands.UpsertSchedule;
using Universe.Application.TeachingSessionServices.Queries.GetSchedule;
using Universe.Core.Constants;

namespace Universe.Api.Controllers;


[Route("programs/{programId:guid}/semesters/{semesterId:guid}/schedule")]
[ApiController, Authorize]
public class ScheduleController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AllRoles)]
    public async Task<IActionResult> GetSchedule(
        [FromRoute] Guid programId,
        [FromRoute] Guid semesterId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetScheduleQuery(programId, semesterId), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> UpdateSchedule(
        [FromRoute] Guid programId,
        [FromRoute] Guid semesterId,
        [FromBody] UpsertScheduleCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { ProgramId = programId, SemesterId = semesterId };
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}