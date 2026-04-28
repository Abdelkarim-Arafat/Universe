using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.AcademicServiceRequestServices.Commands.AcceptServiceRequest;
using Universe.Application.AcademicServiceRequestServices.Commands.RejectServiceRequest;
using Universe.Application.AcademicServiceRequestServices.Queries.GetAllServiceRequests;
using Universe.Application.AcademicServiceRequestServices.Queries.GetServiceRequestHistory;
using Universe.Application.AcademicServiceRequestServices.Queries.GetStudentServiceRequestHistory;
using Universe.Application.Common;

namespace Universe.Api.Controllers;

[Route("service-requests")]
[ApiController , Authorize]
public class ServiceRequestController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPatch("{requestId:guid}/accept")]
    public async Task<IActionResult> AcceptRequest (
        [FromRoute] Guid requestId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new AcceptServiceRequestCommand(requestId), cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpPatch("{requestId:guid}/reject")]
    public async Task<IActionResult> RejectRequest(
        [FromRoute] Guid requestId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RejectServiceRequestCommand(requestId), cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetServiceRequestHistory(
     [FromQuery] Guid collegeId,
     [FromQuery] FilterRequest filter,
     CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetServiceRequestHistoryQuery(collegeId, filter), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("student-history")]
    public async Task<IActionResult> GetStudentServiceRequestHistory(
    [FromQuery] FilterRequest filter,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetStudentServiceRequestHistoryQuery(filter), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllServiceRequests(
    [FromQuery] Guid collegeId,
    [FromQuery] FilterRequest filter,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllServiceRequestsQuery(collegeId, filter), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}
