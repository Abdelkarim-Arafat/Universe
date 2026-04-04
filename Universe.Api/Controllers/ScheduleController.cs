using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.TeachingSessionServices.Commands.UpsertSchedule;
using Universe.Application.TeachingSessionServices.Queries.GetSchedule;

namespace Universe.Api.Controllers;

[ApiController]
[Route("programs/{programId:guid}/semesters/{semesterId:guid}/schedule")]
public class ScheduleController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("")]
    public async Task<IActionResult> GetSchedule(
        [FromRoute] Guid programId,
        [FromRoute] Guid semesterId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetScheduleCommand(programId, semesterId), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("")]
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