using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.Common;
using Universe.Application.ExamServices.ExamTermServices.Commands.Create;
using Universe.Application.ExamServices.ExamTermServices.Commands.Delete;
using Universe.Application.ExamServices.ExamTermServices.Commands.TogglePublisher;
using Universe.Application.ExamServices.ExamTermServices.Commands.Update;
using Universe.Application.ExamServices.ExamTermServices.Queries.Get;
using Universe.Application.ExamServices.ExamTermServices.Queries.GetProgramExams;
using Universe.Core.Enums;

namespace Universe.Api.Controllers.Exams;

[Route("programs/{academicProgramId:Guid}/exam-terms")]
[ApiController, Authorize]
public class ExamTermController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetProgramExams(
        [FromRoute] Guid academicProgramId,
        [FromQuery] Guid SemesterId,
        [FromQuery] FilterRequest filter,
         CancellationToken cancellationToken)
    {
        var request = new GetProgramExamsQuery(academicProgramId, SemesterId, filter);

        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetExamTermQuery(id), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromQuery] Guid SemesterId,
        [FromRoute] Guid academicProgramId,
        [FromBody] CreateExamTermCommand command, CancellationToken cancellationToken)
    {
        command = command with { SemesterId = SemesterId, AcademicProgramId = academicProgramId };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(Get),
            new { academicProgramId = academicProgramId, id = result.Value.Id }, result.Value)
            : result.ToProblem();
    }



    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteExamTermCommand(id), cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdateExamTermCommand command, CancellationToken cancellationToken)
    {
        command = command with { Id = id };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpPatch("{id:Guid}/toggle-publisher")]
    public async Task<IActionResult> TogglePublisher([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new TogglePublisherCommand(id), cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
