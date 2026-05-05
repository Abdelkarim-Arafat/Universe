using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MimeKit.IO;
using Universe.Api.Extensions;
using Universe.Application.Common;
using Universe.Application.StudyLoadByLevelServices.Commands.AddStudyLoad;
using Universe.Application.StudyLoadByLevelServices.Commands.RemoveStudyLoad;
using Universe.Application.StudyLoadByLevelServices.Commands.UpdateStudyLoad;
using Universe.Application.StudyLoadByLevelServices.Queries.GetAll;
using Universe.Core.Constants;
namespace Universe.Api.Controllers;

[Route("programs/{programId:guid}/study-load-by-levels")]
[ApiController]
public class StudyLoadByLevelController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> GetAll (
        [FromQuery] FilterRequest filter,
        [FromRoute] Guid programId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetStudyLoadByLevelsQuery(programId, filter), cancellationToken);
        return result.IsSuccess? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("{levelId:guid}")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> Add (
        [FromRoute] Guid programId,
        [FromRoute] Guid levelId,
        [FromBody] AddStudyLoadByLevelCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { ProgramId = programId, LevelId = levelId };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }
    [HttpPut("{id:guid}")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> Update(
        [FromRoute] Guid programId,
        [FromRoute] Guid id,
        [FromBody] UpdateStudyLoadByLevelCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { Id = id, ProgramId = programId };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpDelete("{id:guid}")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid programId,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RemoveStudyLoadByLevelCommand(programId, id), cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }
}