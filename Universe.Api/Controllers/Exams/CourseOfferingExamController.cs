using MediatR;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.Common;
using Universe.Application.CourseOfferingExamServices.Commands.Create;
using Universe.Application.CourseOfferingExamServices.Commands.Delete;
using Universe.Application.CourseOfferingExamServices.Commands.Update;
using Universe.Application.CourseOfferingExamServices.Queries.Get;
using Universe.Application.ExamServices.CourseOfferingExamServices.Commands.UpsertCourseExamCommittees;

namespace Universe.Api.Controllers.Exams;

[Route("exam-terms/{examTermId:guid}/course-offering-exam")]
[ApiController]
public class CourseOfferingExamController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{courseOfferingId:guid}")]

    public async Task<IActionResult> Get(
        [FromRoute] Guid courseOfferingId,
        [FromRoute] Guid examTermId,
        [FromQuery] FilterRequest filter,
        CancellationToken cancellationToken)
    {
        var query = new GetCourseOfferingExamQuery(courseOfferingId, examTermId, filter);

        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost]
    public async Task<IActionResult> Add
        (
        [FromRoute] Guid examTermId,
        [FromQuery] Guid courseOfferingId,
        [FromBody] CreateCourseOfferingExamCommand command,
        CancellationToken cancellationToken)

    {
        command = command with { ExamTermId = examTermId, CourseOfferingId = courseOfferingId };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(Get),
            new { examTermId = examTermId, courseOfferingId = courseOfferingId }, result.Value)
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
    public async Task<IActionResult> Update([FromRoute] Guid id,
        [FromBody] UpdateCourseOfferingExamCommand command,
        CancellationToken cancellationToken)
    {
        command = command with { Id = id };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("upsert-committees/{courseOfferingExamId:guid}")]
    public async Task<IActionResult> UpsertCommittees(
    [FromRoute] Guid courseOfferingExamId,
    [FromBody] UpsertCourseExamCommitteesRequest request,
    [FromQuery] FilterRequest filter,
    CancellationToken cancellationToken)
    {
        var command = new UpsertCourseExamCommitteesCommand(courseOfferingExamId, request.ExamCommitteesIds, filter);

        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}