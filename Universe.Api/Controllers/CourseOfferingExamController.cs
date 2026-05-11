using MediatR;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.Common;
using Universe.Application.CourseOfferingExamServices.Commands.Create;
using Universe.Application.CourseOfferingExamServices.Commands.Delete;
using Universe.Application.CourseOfferingExamServices.Commands.Update;
using Universe.Application.CourseOfferingExamServices.Queries.Get;
using Universe.Application.ExamServices.CourseOfferingExamServices.Queries.GetCourseExamCommittees;

namespace Universe.Api.Controllers;

[Route("exam-terms/{examTermId:guid}/course-offering-exam")]
[ApiController]
public class CourseOfferingExamController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id:guid}")]

    public async Task<IActionResult> Get(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetCourseOfferingExamQuery(id);

        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost]
    public async Task<IActionResult> Add
        (
        [FromRoute] Guid examTermId,
        [FromQuery] Guid courseOfferingId,
        [FromBody] CreateCourseOfferingExamRequest request,
        CancellationToken cancellationToken)

    {
        var command = new CreateCourseOfferingExamCommand
            (
            request.Date,
            request.StartTime,
            request.EndTime,
            courseOfferingId,
            examTermId,
            request.ExamCommitteesIds
            );

        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(Get),
            new { examTermId = examTermId, id = result.Value.Id }, result.Value)
            : result.ToProblem();
    }

    [HttpDelete("{id:guid}")]

    public async Task<IActionResult> Delete([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteCourseOfferingExamCommand(id), cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateCourseOfferingExamRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCourseOfferingExamCommand(
            id,
            request.Date,
            request.StartTime,
            request.EndTime,
            request.ExamCommitteesIds);

        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess
           ? Ok(result.Value)
           : result.ToProblem();
    }

    [HttpGet("{id:guid}/committees")]
    public async Task<IActionResult> GetCourseCommittees(
        [FromRoute] Guid id,
        [FromQuery] FilterRequest filter,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCourseExamCommitteesQuery(id, filter), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}