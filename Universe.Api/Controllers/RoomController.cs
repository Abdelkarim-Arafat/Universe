using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.RoomServices.Commands.CreateRoom;
using Universe.Application.RoomServices.Commands.DeleteRoom;
using Universe.Application.RoomServices.Commands.UpdateRoom;
using Universe.Application.RoomServices.Queries.GetAllRooms;
using Universe.Application.RoomServices.Queries.GetRoom;

namespace Universe.Api.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class RoomController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

  
    [HttpPost("{buildingId}")]
    public async Task<IActionResult> Add([FromBody] CreateRoomCommand command, Guid buildingId, CancellationToken cancellationToken)
    {
        command = command with { BuildingId = buildingId };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value)
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
    [HttpGet("all")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllRoomsQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

}
