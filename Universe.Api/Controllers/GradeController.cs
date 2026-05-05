using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Universe.Api.Extensions;
using Universe.Application.Common;
using Universe.Application.GradeServices.Commands.Create;
using Universe.Application.GradeServices.Commands.Delete;
using Universe.Application.GradeServices.Commands.Update;
using Universe.Application.GradeServices.Queries.GetGradesByProgram;
using Universe.Application.GradeServices.Queries.Get;
using Universe.Core.Constants;

namespace Universe.Api.Controllers;

[Route("programs/{academicProgramId:guid}/grades")]
[ApiController,Authorize]
public class GradeController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id}")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisorOrStaff)]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = default)
    {
        var query = new GetGradeQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisorOrStaff)]
    public async Task<IActionResult> GetProgramGrades
        (Guid academicProgramId, [FromQuery] FilterRequest filter, CancellationToken cancellationToken = default)
    {
        var query = new GetGradesByProgramQuery(academicProgramId, filter);
        var result = await _mediator.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> Create([FromBody] CreateGradeCommand command, Guid academicProgramId, CancellationToken cancellationToken = default)
    {
        command = command with { AcademicProgramId = academicProgramId };
        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(Get),
            new { academicProgramId = academicProgramId, id = result.Value.Id }, result.Value)
            : result.ToProblem();
    }
    [HttpPut("{id}")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> Update([FromBody] UpdateGradeCommand command, Guid id, CancellationToken cancellationToken = default)
    {
        command = command with { Id = id };
        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpDelete("{id}")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> Remove(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new DeleteGradeCommand(id);
        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
