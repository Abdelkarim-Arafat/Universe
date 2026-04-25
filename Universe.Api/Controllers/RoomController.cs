using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.Common;
using Universe.Application.RoomServices.Commands.CreateRoom;
using Universe.Application.RoomServices.Commands.DeleteRoom;
using Universe.Application.RoomServices.Commands.UpdateRoom;
using Universe.Application.RoomServices.Queries.GetAvailableRoomsForExam;
using Universe.Application.RoomServices.Queries.GetBuildingRooms;
using Universe.Application.RoomServices.Queries.GetRoom;

namespace Universe.Api.Controllers;

[Route("buildings/{buildingId:guid}/rooms")]
[ApiController,Authorize]
public class RoomController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

  
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateRoomCommand command, Guid buildingId, CancellationToken cancellationToken)
    {
        command = command with { BuildingId = buildingId };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteRoomCommand(id);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(UpdateRoomCommand command, Guid id, CancellationToken cancellationToken)
    {
        command = command with { Id = id };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetRoomQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet]
    public async Task<IActionResult> GetBuildingRooms(Guid buildingId, [FromQuery] FilterRequest filter, CancellationToken cancellationToken)
    {
        var query = new GetBuildingRoomsQuery(buildingId, filter);
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("available-for-exam")]
    public async Task<IActionResult> GetAvailableRoomsForExam
       ([FromQuery] Guid CourseOfferingExamId,
        [FromRoute] Guid buildingId,
        [FromQuery] FilterRequest filter,
        CancellationToken cancellationToken)
    {
        var query = new GetAvailableRoomsForExamQuery(buildingId, CourseOfferingExamId, filter);
        var result = await _mediator.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}