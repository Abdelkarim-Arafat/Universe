using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Universe.Api.Extensions;
using Universe.Application.AcademicServiceRequestServices.Commands.AcceptServiceRequest;
using Universe.Application.AcademicServiceRequestServices.Commands.RejectServiceRequest;
using Universe.Application.AcademicServiceRequestServices.Queries.GetAllServiceRequests;
using Universe.Application.AcademicServiceRequestServices.Queries.GetServiceRequestHistory;
using Universe.Application.AcademicServiceRequestServices.Queries.GetStudentServiceRequestHistory;
using Universe.Application.Common;
using Universe.Core.Constants;

namespace Universe.Api.Controllers;

[Route("colleges/{collegeId:guid}/service-requests")]
[ApiController]
public class ServiceRequestController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPatch("{requestId:guid}/accept")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> AcceptRequest (
        [FromRoute] Guid collegeId,
        [FromRoute] Guid requestId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new AcceptServiceRequestCommand(collegeId, requestId), cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpPatch("{requestId:guid}/reject")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> RejectRequest(
        [FromRoute] Guid collegeId,
        [FromRoute] Guid requestId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RejectServiceRequestCommand(collegeId, requestId), cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpGet("history")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> GetServiceRequestHistory(
     [FromRoute] Guid collegeId,
     [FromQuery] FilterRequest filter,
     CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetServiceRequestHistoryQuery(collegeId, filter), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("student-history")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.Student)]
    public async Task<IActionResult> GetStudentServiceRequestHistory(
    [FromQuery] FilterRequest filter,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetStudentServiceRequestHistoryQuery(filter), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> GetAllServiceRequests (
    [FromRoute] Guid collegeId,
    [FromQuery] FilterRequest filter,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllServiceRequestsQuery(collegeId, filter), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}
