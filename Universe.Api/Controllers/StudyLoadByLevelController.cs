using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.StudyLoadByLevelServices.Commands.AddStudyLoad;
using Universe.Application.StudyLoadByLevelServices.Commands.RemoveStudyLoad;
using Universe.Application.StudyLoadByLevelServices.Commands.UpdateStudyLoad;
using Universe.Application.StudyLoadByLevelServices.Queries.GetAll;
namespace Universe.Api.Controllers;

[Route("programs/{programId:guid}/study-load-by-levels")]
[ApiController, Authorize]
public class StudyLoadByLevelController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("")]
    public async Task<IActionResult> GetAll (
        [FromRoute] Guid programId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllStudyLoadByLevelCommand(programId), cancellationToken);

        return result.IsSuccess? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("{levelId:guid}")]
    public async Task<IActionResult> Add(
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
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateStudyLoadByLevelCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { Id = id };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RemoveStudyLoadByLevelCommand(id), cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }
}