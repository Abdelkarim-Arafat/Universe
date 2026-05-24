using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Universe.Api.Extensions;
using Universe.Application.Common;
using Universe.Application.StaffServices.Commands.AssignAdvisorToStudents;
using Universe.Application.StaffServices.Commands.RegisterStaff;
using Universe.Application.StaffServices.Commands.RemoveStaff;
using Universe.Application.StaffServices.Commands.UpdateStaff;
using Universe.Application.StaffServices.Queries.GetAdvisorStudents;
using Universe.Application.StaffServices.Queries.GetAllStaff;
using Universe.Application.StaffServices.Queries.GetStaff;
using Universe.Core.Constants;

namespace Universe.Api.Controllers;

[Route("colleges/{collegeId:guid}/stuff")]
[ApiController, Authorize]

public class StuffController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;


    [HttpPut("{advisorId:guid}/assign-advisor")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> AssignAdvisorToStudents(
        [FromRoute] Guid advisorId,
        [FromBody] AssignAdvisorToStudentsCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { AdvisorId = advisorId };
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpGet("")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> GetAllStuff(
        [FromRoute] Guid collegeId,
        [FromQuery] FilterRequest filter,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllStaffQuery(collegeId, filter), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("{id:guid}")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisorOrStaff)]
    public async Task<IActionResult> GetStuff(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetStaffQuery(id), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }


    [HttpGet("advisor-students")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisorOrStaff)]
    public async Task<IActionResult> GetAdvisorStudents(
        [FromQuery] Guid? advisorId,
        [FromQuery] FilterRequest filter,
        CancellationToken cancellationToken)
    {
        if (advisorId == null) advisorId = Guid.Parse(User.GetUserId()!);

        var request = new GetAdvisorStudentsQuery(advisorId.Value, filter);
        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]

    public async Task<IActionResult> RegisterStuff(
        [FromRoute] Guid collegeId,
        [FromBody] RegisterStaffCommand request,
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

    public async Task<IActionResult> UpdateStaff(
        [FromRoute] Guid id,
        [FromBody] UpdateStaffCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { UserId = id };
        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpDelete("{id:guid}")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> RemoveStuff(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RemoveStaffCommand(id), cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }
}
