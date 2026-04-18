using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.Common;
using Universe.Application.GradeServices.Commands.CreateGrade;
using Universe.Application.GradeServices.Commands.DeleteGrade;
using Universe.Application.GradeServices.Commands.UpdateGrade;
using Universe.Application.GradeServices.Queries.GetAcademicProgramGrades;
using Universe.Application.GradeServices.Queries.GetGrade;
 

namespace Universe.Api.Controllers;

[Route("programs/{academicProgramId:guid}/grades")]
[ApiController]
[Authorize()]
public class GradeController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = default)
    {
        var query = new GetGradeQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll(Guid academicProgramId, [FromQuery] FilterRequest filter, CancellationToken cancellationToken = default)
    {
        var query = new GetAcademicProgramGradesQuery(academicProgramId, filter);
        var result = await _mediator.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGradeCommand command, Guid academicProgramId, CancellationToken cancellationToken = default)
    {
        command = command with { AcademicProgramId = academicProgramId };
        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value)
            : result.ToProblem();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] UpdateGradeCommand command, Guid id, CancellationToken cancellationToken = default)
    {
        command = command with { Id = id };
        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new DeleteGradeCommand(id);
        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
