using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.ComponentModel.DataAnnotations;
using Universe.Api.Extensions;
using Universe.Application.CourseOfferingServices.Commands.RemoveCourseOeffering;
using Universe.Application.TeachingSessionServices.Commands.AddSession;
using Universe.Application.TeachingSessionServices.Commands.RemoveSession;
using Universe.Application.TeachingSessionServices.Queries.GetCourseSessions;
using Universe.Application.TeachingSessionServices.Queries.GetInstructorSessions;
using Universe.Core.Constants;
using Universe.Core.Enums;

namespace Universe.Api.Controllers;

[Route("teaching-sessions")]
[ApiController, Authorize]
public class TeachingSessionsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> AddSession (
        [FromBody] AddSessionCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpDelete("{sessionId:guid}")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> RemoveSession(
        [FromRoute] Guid sessionId,
        [FromQuery] Guid courseOfferingId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RemoveSessionCommand(sessionId , courseOfferingId), cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpGet("")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AllRoles)]
    public async Task<IActionResult> GetCourseSessions (
        [FromQuery] Guid courseOfferingId,
        [FromQuery] int groupNumber,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCourseSessionsQuery(courseOfferingId , groupNumber), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("instructor-sessions")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisorOrStaff)]
    public async Task<IActionResult> GetInstructorSessions(
        [FromQuery] Guid programId,
        [FromQuery] Guid academicYearId,
        [FromQuery] TermType termType,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetInstructorSessionsQuery(programId, academicYearId, termType), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}