using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.CourseOfferingExamServices.Commands.Create;
using Universe.Application.CourseOfferingExamServices.Commands.Delete;
using Universe.Application.CourseOfferingExamServices.Commands.Update;
using Universe.Application.CourseOfferingExamServices.Queries.Get;

namespace Universe.Api.Controllers.Exams;

[Route("exam-terms/{examTermId:guid}/course-offering-exam")]
[ApiController]
public class CourseOfferingExamController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id:guid}")]

    public async Task<IActionResult> Get([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetCourseOfferingExamQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost]
    public async Task<IActionResult> Add
        ([FromRoute] Guid examTermId,
        [FromQuery] Guid courseOfferingId,
        [FromBody] CreateCourseOfferingExamCommand command,
        CancellationToken cancellationToken)

    {
        command = command with { ExamTermId = examTermId, CourseOfferingId = courseOfferingId };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(Get), new { examTermId = examTermId, id = result.Value.Id }, result.Value)
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
}