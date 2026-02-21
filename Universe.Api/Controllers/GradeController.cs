using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.GradeServices.Commands.CreateGrade;
using Universe.Application.GradeServices.Commands.DeleteGrade;
using Universe.Application.GradeServices.Commands.UpdateGrade;
using Universe.Application.GradeServices.Queries.GetCollegeGrades;
using Universe.Application.GradeServices.Queries.GetGrade;

namespace Universe.Api.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize()]
public class GradeController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid Id, CancellationToken cancellationToken = default)
    {
        var query = new GetGradeQuery(Id);
        var result = await _mediator.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpGet("{CollegeId}-all")]
    public async Task<IActionResult> GetAll(Guid CollegeId, CancellationToken cancellationToken = default)
    {
        var query = new GetCollegeGradesQuery(CollegeId);
        var result = await _mediator.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPost("{CollegeId}-create")]
    public async Task<IActionResult> Create([FromBody] CreateGradeCommand command, Guid CollegeId, CancellationToken cancellationToken = default)
    {
        command = command with { CollegeId = CollegeId };
        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess ? CreatedAtAction(nameof(Get), new { result.Value.Id }, result.Value) : result.ToProblem();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] UpdateGradeCommand command, Guid Id, CancellationToken cancellationToken = default)
    {
        command = command with { Id = Id };
        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(Guid Id, CancellationToken cancellationToken = default)
    {
        var command = new DeleteGradeCommand(Id);
        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
