using MediatR;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.ControlServices.Commands.ToggleAnnounceResult;
using Universe.Application.ControlServices.Commands.ToggleCourseOfferingControl;
using Universe.Application.ControlServices.Commands.UpsertStudentDegree;
using Universe.Application.ControlServices.Queries;
using Universe.Application.ControlServices.Queries.GetStudents;
 

namespace Universe.Api.Controllers;

[Route("control")]
[ApiController]

/*
 public record GetLevelsCoursesStatisticsQuery(
    [Required] Guid SemesterId,
    [Required] Guid ProgramId
) : IRequest<Result<List<LevelCoursesStatisticsResponse>>>;
 */
public class ControlController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;


    [HttpGet("control-status")]
    public async Task<IActionResult> GetCourseOfferingControlStatus(
        [FromQuery] Guid programId,
        [FromQuery] Guid semesterId,
        CancellationToken cancellationToken)
    {
        var request = new GetCourseOfferingsControlStatisticsQuery(programId , semesterId);
        var result = await _mediator.Send(request , cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }


    [HttpPatch("{courseOfferingId:guid}/toggle-control")]
    public async Task<IActionResult> ToggleCourseOfferingControl(
        [FromRoute]Guid courseOfferingId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ToggleCourseOfferingControlCommand(courseOfferingId), cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpGet("{AcademicProgramId:guid}")]
    public async Task<IActionResult> GetStudents([FromQuery] GetStudentsCommand request,
        Guid AcademicProgramId, CancellationToken cancellationToken)
    {
        request = request with { AcademicProgramId = AcademicProgramId };
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }
    [HttpPatch("{AcademicProgramId:guid}")]
    public async Task<IActionResult> UpsertStudentsDegree(
       Guid AcademicProgramId,
       [FromBody] UpsertStudentDegreeCommand command,  
       CancellationToken cancellationToken)
    {
        var updatedRequest = command with { AcademicProgramId = AcademicProgramId};

        var result = await _mediator.Send(updatedRequest, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetLevelsCoursesStatistics(
       [FromQuery] Guid academicProgramId,
       [FromQuery] Guid semesterId,
       CancellationToken cancellationToken)
    {

        var result = await _mediator.Send(new GetCourseOfferingsControlStatisticsQuery(academicProgramId, semesterId), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }
    [HttpPatch("toggle-announce-result")]
    public async Task<IActionResult> ToggleAnnounceResult([FromQuery] Guid SemesterId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ToggleAnnounceResultCommand(SemesterId), cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }
}
