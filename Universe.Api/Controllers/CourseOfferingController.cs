using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Universe.Api.Extensions;
using Universe.Application.Common;
using Universe.Application.CourseOfferingServices.Commands.AddCourseOffering;
using Universe.Application.CourseOfferingServices.Commands.RemoveCourseOeffering;
using Universe.Application.CourseOfferingServices.Commands.UpdateCourseOffering;
using Universe.Application.CourseOfferingServices.Queries.GetCourseOffering;
using Universe.Application.CourseOfferingServices.Queries.GetCourseOfferingAssessments;
using Universe.Application.CourseOfferingServices.Queries.GetLevelCourses;
using Universe.Application.CourseOfferingServices.Queries.GetProgramCoursesForExams;
using Universe.Core.Enums;

namespace Universe.Api.Controllers;

[Route("programs/{programId:guid}/course-offerings")]
[ApiController]

public class CourseOfferingController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id:guid}")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisorOrStaff)]
    public async Task<IActionResult> GetCourseOffering(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCourseOfferingCommand(id), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisorOrStaff)]
    public async Task<IActionResult> GetLevelCourses (
        [FromRoute] Guid programId,
        [FromQuery] Guid levelId,
        [FromQuery] Guid academicYearId,
        [FromQuery] TermType semesterType,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetLevelCoursesQuery(programId, levelId, academicYearId, semesterType), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> AddCourseOffering (
        [FromRoute] Guid programId,
        [FromBody] AddCourseOfferingCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { AcademicProgramId = programId };

        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("{id:guid}")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> UpdateCourseOffering (
        [FromRoute] Guid id,
        [FromRoute] Guid programId,
        [FromBody] UpdateCourseOfferingCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { Id = id , AcademicProgramId = programId };
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpDelete("{id:guid}")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> RemoveCourseOffering(
        [FromRoute] Guid programId,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RemoveCourseOfferingCommand(programId, id), cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpGet("for-exams")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisorOrStaff)]
    public async Task<IActionResult> GetProgramCoursesForExams(
        [FromRoute] Guid programId,
        [FromQuery] Guid semesterId,
        [FromQuery] Guid examTermId,
        [FromQuery] FilterRequest filter,
        CancellationToken cancellationToken)
    {
        var query = new GetProgramCoursesForExamsQuery(programId, semesterId, examTermId, filter);
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpGet("{id:guid}/assessments")]
    public async Task<IActionResult> GetCourseOfferingAssessments(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetCourseOfferingAssessmentsQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}