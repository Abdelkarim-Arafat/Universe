using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.Common;
using Universe.Application.CourseOfferingServices.Commands.AddCourseOffering;
using Universe.Application.CourseOfferingServices.Commands.RemoveCourseOeffering;
using Universe.Application.CourseOfferingServices.Commands.UpdateCourseOffering;
using Universe.Application.CourseOfferingServices.Query.GetCourseOffering;
using Universe.Application.CourseOfferingServices.Query.GetLevelCourses;
using Universe.Application.CourseOfferingServices.Query.GetProgramCoursesForExams;
using Universe.Core.Enums;

namespace Universe.Api.Controllers;

[Route("programs/{programId:guid}/course-offerings")]
[ApiController , Authorize]
public class CourseOfferingController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCourseOffering (
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCourseOfferingCommand(id), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet]
    public async Task<IActionResult> GetLevelCourses (
        [FromQuery] Guid levelId,
        [FromQuery] Guid academicYearId,
        [FromQuery] TermType semesterType,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetLevelCoursesCommand(levelId , academicYearId , semesterType), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost]
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
    public async Task<IActionResult> RemoveCourseOffering(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RemoveCourseOfferingCommand(id), cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpGet("for-exams")]
    public async Task<IActionResult> GetProgramCoursesForExams(
        [FromRoute] Guid programId,
        [FromQuery] Guid semesterId,
        [FromQuery] FilterRequest filter,
        CancellationToken cancellationToken)
    {
        var query = new GetProgramCoursesForExamsQuery(programId, semesterId, filter);
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}