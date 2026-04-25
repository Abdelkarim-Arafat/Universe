using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.Common;
using Universe.Application.LevelServices.Commands.CreateLevel;
using Universe.Application.LevelServices.Commands.RemoveLevel;
using Universe.Application.LevelServices.Commands.UpdateLevel;
using Universe.Application.LevelServices.Queries.GetAcademicProgramLevels;
using Universe.Application.LevelServices.Queries.GetLevel;

namespace Universe.Api.Controllers;

[Route("programs/{academicProgramId:guid}/levels")]
[ApiController,Authorize]
public class LevelController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    [HttpGet()]
    public async Task<IActionResult> GetAll(Guid academicProgramId, [FromQuery] FilterRequest filter, CancellationToken cancellationToken = default)
    {
        var request = new GetAcademicProgramLevelsQuery(academicProgramId, filter);
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = default)
    {
        var request = new GetLevelQuery(id);
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLevelCommand command, Guid academicProgramId, CancellationToken cancellationToken = default)
    {
        command = command with { AcademicProgramId = academicProgramId };

        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new RemoveLevelCommand(id);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] UpdateLevelCommand command, Guid id, CancellationToken cancellationToken = default)
    {
        command = command with { Id = id };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}