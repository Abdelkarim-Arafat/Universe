using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Universe.Api.Extensions;
using Universe.Application.Common;
using Universe.Application.LevelServices.Commands.CreateLevel;
using Universe.Application.LevelServices.Commands.RemoveLevel;
using Universe.Application.LevelServices.Commands.UpdateLevel;
using Universe.Application.LevelServices.Queries.GetAcademicProgramLevels;
using Universe.Application.LevelServices.Queries.GetLevel;
using Universe.Core.Constants;
using Universe.Core.Entities;

namespace Universe.Api.Controllers;

[Route("programs/{academicProgramId:guid}/levels")]
[ApiController]
public class LevelController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisorOrStaff)]
    public async Task<IActionResult> GetAll(
        [FromRoute] Guid academicProgramId,
        [FromQuery] FilterRequest filter,
        CancellationToken cancellationToken = default)
    {
        var request = new GetAcademicProgramLevelsQuery(academicProgramId, filter);
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisorOrStaff)]
    public async Task<IActionResult> Get(
        [FromRoute] Guid id,
        [FromRoute] Guid academicProgramId,
        CancellationToken cancellationToken = default)
    {
        var request = new GetLevelQuery(academicProgramId, id);
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> Create(
        [FromBody] CreateLevelCommand command,
        Guid academicProgramId, CancellationToken cancellationToken = default)
    {
        command = command with { AcademicProgramId = academicProgramId };

        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpDelete("{id}")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> Remove(
        [FromRoute] Guid academicProgramId,
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var command = new RemoveLevelCommand(academicProgramId, id);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpPut("{id}")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> Update(
        [FromBody] UpdateLevelCommand command,
        [FromRoute] Guid academicProgramId,
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        command = command with { ProgramId = academicProgramId, Id = id };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}