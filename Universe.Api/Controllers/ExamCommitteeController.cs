using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Universe.Api.Extensions;
using Universe.Application.Common;
using Universe.Application.ExamCommitteeServices.Commands.Create;
using Universe.Application.ExamCommitteeServices.Commands.Delete;
using Universe.Application.ExamCommitteeServices.Commands.Update;
using Universe.Application.ExamCommitteeServices.Queries.Get;
using Universe.Application.ExamCommitteeServices.Queries.GetExamTermCommittees;
using Universe.Core.Constants;

namespace Universe.Api.Controllers;

[Route("exam-terms/{examTermId:Guid}/committees")]
[ApiController,Authorize]
public class ExamCommitteeController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;


    [HttpPost]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> Add (
        [FromRoute] Guid examTermId,
        [FromQuery] Guid RoomId,
        [FromBody] CreateExamCommitteeCommand command, CancellationToken cancellationToken)
    {
        command = command with { RoomId = RoomId, ExamTermId = examTermId };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(Get), new { examTermId = examTermId, id = result.Value.Id }, result.Value)
            : result.ToProblem();
    }

    [HttpGet("{id:guid}")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetExamCommitteeQuery(id), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpDelete("{id:guid}")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteExamCommitteeCommand(id), cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("{id:guid}")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> Update(
       [FromRoute] Guid examTermId, [FromRoute] Guid id,
       [FromBody] UpdateExamCommitteeCommand command, CancellationToken cancellationToken)
    {
        command = command with { Id = id };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpGet]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> GetExamTermCommittees(
        [FromRoute] Guid examTermId,
        [FromQuery] FilterRequest filter,
        CancellationToken cancellationToken)
    {
        var query = new GetExamTermCommitteesQuery(filter, examTermId);
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}
