using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.DepartmentServices.Commands.AddDepartment;
using Universe.Application.DepartmentServices.Commands.RemoveDepartment;
using Universe.Application.DepartmentServices.Commands.UpdateDepartment;
using Universe.Application.DepartmentServices.Query.GetDepartment;
using Universe.Application.DepartmentServices.Query.GetDepartments;

namespace Universe.Api.Controllers;

[Route("college/{collegeId:guid}/department")]
[ApiController, Authorize(Roles = "Admin , AcadimicAdvising")]
public class DepartmentController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetDepartmentCommand(id) , cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll([FromRoute] Guid collegeId , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetDepartmentsCommand(collegeId), cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPost("")]
    public async Task<IActionResult> Add([FromRoute] Guid collegeId , [FromBody] AddDepartmentCommand request , CancellationToken cancellationToken)
    {
        request = request with { CollegeId = collegeId };

        var result = await _mediator.Send(request , cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid collegeId , [FromRoute] Guid id, UpdateDepartmentCommand request , CancellationToken cancellationToken)
    {
        request = request with { CollegeId = collegeId , Id = id };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RemoveDepartmentCommand(id), cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }
}
