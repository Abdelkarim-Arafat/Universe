using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Universe.Api.Extensions;
using Universe.Application.AcademicServiceServices.Commands.Add_Service;
using Universe.Application.AcademicServiceServices.Commands.CreateOrder;
using Universe.Application.AcademicServiceServices.Commands.RemoveService;
using Universe.Application.AcademicServiceServices.Commands.UpdateService;
using Universe.Application.AcademicServiceServices.Queries.GetAllServices;
using Universe.Application.Common;
using Universe.Core.Constants;

namespace Universe.Api.Controllers;

[Route("colleges/{collegeId:guid}/services")]
[ApiController]
public class ServiceController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> AddService(
        [FromRoute] Guid collegeId,
        [FromBody] AddServiceCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { CollegeId = collegeId };
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("{serviceId:guid}/checkout")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = $"{Roles.AdminOrAdvisorOrStaff} , {Roles.Student}")]
    public async Task<IActionResult> CreateOrder (
        [FromRoute] Guid collegeId,
        [FromRoute] Guid serviceId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateOrderCommand(collegeId, serviceId), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = $"{Roles.AdminOrAdvisorOrStaff} , {Roles.Student}")]
    public async Task<IActionResult> GetAllServices (
        [FromRoute] Guid collegeId,
        [FromQuery] FilterRequest filter,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetServicesQuery(collegeId , filter), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("{serviceId:guid}")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> UpdateService(
        [FromRoute] Guid collegeId,
        [FromRoute] Guid serviceId,
        [FromBody] UpdateServiceCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { CollegeId = collegeId, Id = serviceId };

        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpDelete("{serviceId:guid}")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> RemoveService(
        [FromRoute] Guid collegeId,
        [FromRoute] Guid serviceId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RemoveServiceCommand(collegeId, serviceId), cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
}