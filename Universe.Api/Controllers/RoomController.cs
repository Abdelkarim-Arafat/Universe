using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Universe.Api.Extensions;
using Universe.Application.Common;
using Universe.Application.RoomServices.Commands.Create;
using Universe.Application.RoomServices.Commands.Delete;
using Universe.Application.RoomServices.Commands.Update;
using Universe.Application.RoomServices.Queries.Get;
using Universe.Application.RoomServices.Queries.GetAvailableRoomsForCommittees;
using Universe.Application.RoomServices.Queries.GetBuildingRooms;
using Universe.Core.Constants;

namespace Universe.Api.Controllers;

[Route("buildings/{buildingId:guid}/rooms")]
[ApiController, Authorize]
public class RoomController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> Add([FromBody] CreateRoomCommand command, [FromRoute] Guid buildingId, CancellationToken cancellationToken)
    {
        command = command with { BuildingId = buildingId };
        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(Get), new { buildingId, id = result.Value.Id }, result.Value)
            : result.ToProblem();
    }

    [HttpDelete("{id:guid}")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> Remove([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteRoomCommand(id);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("{id:guid}")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> Update([FromBody] UpdateRoomCommand command, [FromRoute] Guid id, CancellationToken cancellationToken)
    {
        command = command with { Id = id };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpGet("{id:guid}")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetRoomQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> GetBuildingRooms([FromRoute] Guid buildingId, [FromQuery] FilterRequest filter, CancellationToken cancellationToken)
    {
        var query = new GetBuildingRoomsQuery(buildingId, filter);
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("available-for-committees")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisorOrStaff)]
    public async Task<IActionResult> GetAvailableRoomsForCommittees(
        [FromRoute] Guid buildingId,
        [FromQuery] Guid examTermId,
        [FromQuery] FilterRequest filter,
        CancellationToken cancellationToken)
    {
        var query = new GetAvailableRoomsForCommitteesQuery(buildingId, examTermId, filter);
        var result = await _mediator.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}