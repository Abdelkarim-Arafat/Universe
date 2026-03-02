using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.Common;
using Universe.Application.CourseServices.Commands.AddCourse;
using Universe.Application.CourseServices.Commands.AddCoursePrerequisite;
using Universe.Application.CourseServices.Commands.RemoveCourse;
using Universe.Application.CourseServices.Commands.RemoveCoursePrerequisite;
using Universe.Application.CourseServices.Commands.UpdateCourse;
using Universe.Application.CourseServices.Query.GetAllCourses;
using Universe.Application.CourseServices.Query.GetCourse;
using Universe.Application.CourseServices.Query.GetCoursePrerequisite;

namespace Universe.Api.Controllers;


[Route("college/{collegeId:guid}/course")]
[ApiController , Authorize(Roles = "Admin , AcademicAdvising")]
public class CourseController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCourseCommand(id), cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }
    [HttpGet("")]
    public async Task<IActionResult> GetAll([FromRoute] Guid collegeId, [FromQuery] FilterRequest filter , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllCoursesCommand(collegeId , filter), cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }
    [HttpGet("{id:guid}/pre-requisites")]
    public async Task<IActionResult> GetCoursePreRequisites([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCoursePreRequisiteCommand(id), cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPost("{id:guid}/pre-requisite/{preRequisiteId:guid}")]
    public async Task<IActionResult> AddCoursePreRequisite([FromRoute] Guid id, [FromRoute] Guid preRequisiteId , CancellationToken cancellationToken)
    {

        var result = await _mediator.Send(new AddCoursePreRequisiteCommand(id , preRequisiteId), cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpDelete("{id:guid}/pre-requisite/{preRequisiteId:guid}")]
    public async Task<IActionResult> DeleteCoursePreRequisite([FromRoute] Guid id, [FromRoute] Guid preRequisiteId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RemoveCoursePrerequisiteCommand(id, preRequisiteId), cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpPost("")]
    public async Task<IActionResult> Add([FromRoute] Guid collegeId, [FromBody] AddCourseCommand request, CancellationToken cancellationToken)
    {
        request = request with { CollegeId = collegeId };

        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid collegeId, [FromRoute] Guid id, UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        request = request with { CollegeId = collegeId, Id = id };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RemoveCourseCommand(id) , cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }
}