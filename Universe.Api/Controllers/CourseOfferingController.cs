using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.CourseOfferingServices.Commands.AddCourseOffering;
using Universe.Application.CourseOfferingServices.Commands.RemoveCourseOeffering;
using Universe.Application.CourseOfferingServices.Commands.UpdateCourseOffering;
using Universe.Application.CourseOfferingServices.Dtos;
using Universe.Application.CourseOfferingServices.Query.GetCourseOffering;
using Universe.Application.CourseOfferingServices.Query.GetLevelCourses;

namespace Universe.Api.Controllers;

[Route("programs/{programId:guid}/course-offerings")]
[ApiController]
public class CourseOfferingController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCourseOffering(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCourseOfferingCommand(id), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetLevelCourses(
        GetLevelCoursesCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("")]
    public async Task<IActionResult> AddCourseOffering(
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
}