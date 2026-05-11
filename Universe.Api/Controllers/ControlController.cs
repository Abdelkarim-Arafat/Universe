using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Universe.Api.Extensions;
using Universe.Application.Common;
using Universe.Application.ControlServices.Commands.ToggleAnnounceResult;
using Universe.Application.ControlServices.Commands.ToggleCourseOfferingControl;
using Universe.Application.ControlServices.Commands.UpsertStudentDegree;
using Universe.Application.ControlServices.Queries;
using Universe.Application.ControlServices.Queries.GetStudents;
using Universe.Core.Constants;


namespace Universe.Api.Controllers;

[Route("control")]
[ApiController, Authorize]
public class ControlController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("control-status")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> GetCourseOfferingControlStatus(
        [FromQuery] Guid programId,
        [FromQuery] Guid semesterId,
        CancellationToken cancellationToken)
    {
        var request = new GetCourseOfferingsControlStatisticsQuery(semesterId , programId);
        var result = await _mediator.Send(request , cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }


    [HttpPatch("{courseOfferingId:guid}/toggle-control")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> ToggleCourseOfferingControl(
        [FromRoute]Guid courseOfferingId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ToggleCourseOfferingControlCommand(courseOfferingId), cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
     
    [HttpGet("{AcademicProgramId:guid}")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisorOrStaff)]
    public async Task<IActionResult> GetStudents(
        [FromQuery] GetStudentsRequest request,
        [FromQuery] FilterRequest filter,
        [FromRoute] Guid AcademicProgramId, CancellationToken cancellationToken)
    {
        var command = new GetStudentsCommand(
            AcademicProgramId,
            request.CourseOfferingId,
            request.GroupNumber,
            filter
        );
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPatch("{AcademicProgramId:guid}")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisorOrStaff)]
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

    [HttpPatch("toggle-announce-result")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> ToggleAnnounceResult([FromQuery] Guid SemesterId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ToggleAnnounceResultCommand(SemesterId), cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }
}
