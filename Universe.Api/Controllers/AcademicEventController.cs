using MediatR;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.AcademicEventServices.Commands.Add_Event;
using Universe.Application.AcademicEventServices.Commands.Remove_Event;
using Universe.Application.AcademicEventServices.Queries.Get_All_Events;
using Universe.Application.Common;

namespace Universe.Api.Controllers;

[Route("events")]
[ApiController]

public class AcademicEventController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("")]
    public async Task<IActionResult> AddEvent(
        [FromBody] AddEventCommand request,
        [FromQuery] Guid programId,
        [FromQuery] Guid semesterId,
        CancellationToken cancellationToken)
    {
        request = request with { ProgramId = programId, SemesterId = semesterId };
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> RemoveEvent(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
        )
    {
        var result = await _mediator.Send(new RemoveAcademicEventCommand(id), cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }


    [HttpGet("")]
    public async Task<IActionResult> GetEvents (
        [FromQuery] Guid programId,
        [FromQuery] Guid semesterId,
        [FromQuery] FilterRequest filterRequest,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllEventsCommand(programId, semesterId, filterRequest), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}
