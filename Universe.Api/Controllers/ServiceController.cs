using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.AcademicServiceServices.Commands.Add_Service;
using Universe.Application.AcademicServiceServices.Commands.ChangeServiceRequestStatus;
using Universe.Application.AcademicServiceServices.Commands.RejectServiceRequest;
using Universe.Application.AcademicServiceServices.Commands.RemoveService;
using Universe.Application.AcademicServiceServices.Commands.UpdateService;
using Universe.Application.AcademicServiceServices.Queries.GetAllServiceRequests;
using Universe.Application.AcademicServiceServices.Queries.GetAllServices;
using Universe.Application.AcademicServiceServices.Queries.GetServiceRequestHistory;
using Universe.Application.Common;
using Universe.Application.PaymentService.Commands.CreateOrder;

namespace Universe.Api.Controllers;

[Route("services")]
[ApiController , Authorize]
public class ServiceController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("")]
    public async Task<IActionResult> AddService (
        [FromQuery] Guid collegeId,
        [FromBody] AddServiceCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { CollegeId = collegeId };
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("{serviceId:guid}/checkout")]
    public async Task<IActionResult> CreateOrder (
        [FromRoute] Guid serviceId,
        CancellationToken cancellationToken)
    {

        var result = await _mediator.Send(new CreateOrderCommand(serviceId), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllServices(
        [FromQuery] Guid collegeId,
        [FromQuery] FilterRequest filter,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllServicesQuery(collegeId , filter), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("{serviceId:guid}")]
    public async Task<IActionResult> UpdateService(
        [FromRoute] Guid serviceId,
        [FromBody] UpdateServiceCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { Id = serviceId };

        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpDelete("{serviceId:guid}")]
    public async Task<IActionResult> RemoveService(
        [FromRoute] Guid serviceId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RemoveServiceCommand(serviceId), cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
}