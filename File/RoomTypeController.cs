using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.RoomServices.Commands.CreateRoom;
using Universe.Application.RoomTypeServices.Commands.CreateRoomType;
using Universe.Application.RoomTypeServices.Commands.DeleteRoomType;
using Universe.Application.RoomTypeServices.Commands.UpdateRoomType;
using Universe.Application.RoomTypeServices.Queries.GetAllTypes;
using Universe.Application.RoomTypeServices.Queries.GetRoomType;

namespace Universe.Api.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class RoomTypeController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    // room-Type

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateRoomTypeCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value)
            : result.ToProblem();
    }
    [HttpGet("all")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllTypesQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetRoomTypeQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var query = new DeleteRoomTypeCommand(id);
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(UpdateRoomTypeCommand command, Guid id, CancellationToken cancellationToken)
    {
        command = command with { Id = id };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
