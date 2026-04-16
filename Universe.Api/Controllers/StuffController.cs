using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.Common;
using Universe.Application.UserServices.Commands.AssignAdvisorToStudents;
using Universe.Application.UserServices.Commands.RegisterStaff;
using Universe.Application.UserServices.Commands.RemoveStuff;
using Universe.Application.UserServices.Commands.UpdateStuff;
using Universe.Application.UserServices.Querys.GetAdvisorStudents;
using Universe.Application.UserServices.Querys.GetAllStuff;
using Universe.Application.UserServices.Querys.GetStuff;

namespace Universe.Api.Controllers;

[Route("colleges/{collegeId:guid}/stuff")]
[ApiController , Authorize] 
public class StuffController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;


    [HttpPut("{advisorId:guid}/assign-advisor")]
    public async Task<IActionResult> AssignAdvisorToStudents(
        [FromRoute] Guid advisorId,
        [FromBody]  AssignAdvisorToStudentsCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { AdvisorId = advisorId };
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllStuff(
        [FromRoute] Guid collegeId,
        [FromQuery] FilterRequest filter,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllStuffCommand(collegeId, filter), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetStuff (
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetStuffCommand(id), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("advisor-students")]
    public async Task<IActionResult> GetAdvisorStudents(
        [FromQuery] FilterRequest filter,
        CancellationToken cancellationToken)
    {
        var request = new GetAdvisorStudentsCommand(Guid.Parse(User.GetUserId()!) , filter);
        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }


    [HttpPost("")]
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
    public async Task<IActionResult> UpdateStuff(
        [FromRoute] Guid id,
        [FromBody] UpdateStuffCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { UserId = id };
        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> RemoveStuff(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RemoveStuffCommand(id), cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }
}
