using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Universe.Api.Extensions;
using Universe.Application.Common;
using Universe.Application.CourseServices.Commands.AddCourse;
using Universe.Application.CourseServices.Commands.RemoveCourse;
using Universe.Application.CourseServices.Commands.UpdateCourse;
using Universe.Application.CourseServices.Query.GetAllCourses;
using Universe.Application.CourseServices.Query.GetCourse;
using Universe.Core.Constants;

namespace Universe.Api.Controllers;


[Route("colleges/{collegeId:guid}/courses")]
[ApiController]
public class CourseController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id:guid}")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisorOrStaff)]
    public async Task<IActionResult> Get (
        [FromRoute] Guid collegeId,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCourseQuery(collegeId, id), cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }
    [HttpGet("")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisorOrStaff)]
    public async Task<IActionResult> GetAll(
        [FromRoute] Guid collegeId,
        [FromQuery] FilterRequest filter,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCoursesQuery(collegeId , filter), cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPost("")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> Add (
        [FromRoute] Guid collegeId,
        [FromBody] AddCourseCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { CollegeId = collegeId };

        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }
    [HttpPut("{id:guid}")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> Update(
        [FromRoute] Guid collegeId,
        [FromRoute] Guid id,
        UpdateCourseCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { CollegeId = collegeId, Id = id };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpDelete("{id:guid}")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid collegeId,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RemoveCourseCommand(collegeId, id) , cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }
}