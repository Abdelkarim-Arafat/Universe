using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.GradeServices.Queries.GetCollegeGrades;
using Universe.Application.LevelServices.Commands.CreateLevel;
using Universe.Application.LevelServices.Commands.RemoveLevel;
using Universe.Application.LevelServices.Commands.UpdateLevel;
using Universe.Application.LevelServices.Queries.GetCollegeLevels;
using Universe.Application.LevelServices.Queries.GetLevel;

namespace Universe.Api.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class LevelController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    [HttpGet("{CollegeId}-all")]
    public async Task<IActionResult> GetAll(Guid CollegeId, CancellationToken cancellationToken = default)
    {
        var request = new GetCollegeLevelsQuery(CollegeId);
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> Get(Guid Id, CancellationToken cancellationToken = default)
    {
        var request = new GetLevelQuery(Id);
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("{CollegeId}-create")]
    public async Task<IActionResult> Create([FromBody] CreateLevelCommand command, Guid CollegeId, CancellationToken cancellationToken = default)
    {
        command = command with { CollegeId = CollegeId };

        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess ? CreatedAtAction(nameof(Get), new { result.Value.Id }, result.Value) : result.ToProblem();
    }
    [HttpDelete("remove-{id}")]
    public async Task<IActionResult> Remove(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new RemoveLevelCommand(id);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpPut("update-{id}")]
    public async Task<IActionResult> Update([FromBody] UpdateLevelCommand command, Guid id, CancellationToken cancellationToken = default)
    {
        command = command with { Id = id };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}