using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.BuildingServices.Commands.CreateBuilding;
using Universe.Application.BuildingServices.Commands.DeleteBuilding;
using Universe.Application.BuildingServices.Commands.UpdateBuilding;
using Universe.Application.BuildingServices.Queries.GetAll;
using Universe.Application.BuildingServices.Queries.GetBuilding;
using Universe.Application.Common;
using Universe.Core.Enums;

namespace Universe.Api.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class BuildingController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetBuildingQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpGet("all")]
    public async Task<IActionResult> GetAll([FromQuery] FilterRequest filter,CancellationToken cancellationToken)
    {
        var query = new GetAllQuery(filter);
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBuildingCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new DeleteBuildingCommand(id);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] UpdateBuildingCommand command, Guid id, CancellationToken cancellationToken = default)
    {
        command = command with { Id = id };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpGet("room-types")]
    public async Task<IActionResult> GetRoomTypes(CancellationToken cancellationToken)
    {
        var types = Enum.GetValues(typeof(RoomType))
            .Cast<RoomType>()
            .Select(v => new { Id = (int)v, Name = v.ToString() })
            .ToList();
        return Ok(types);
    }
}
