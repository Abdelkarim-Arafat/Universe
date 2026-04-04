using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.Common;
using Universe.Application.AcademicProgramServices.Commands.AddAcademicProgram;
using Universe.Application.AcademicProgramServices.Commands.RemoveAcademicProgram;
using Universe.Application.AcademicProgramServices.Commands.UpdateAcademicProgram;
using Universe.Application.AcademicProgramServices.Query.GetAcademicProgram;
using Universe.Application.AcademicProgramServices.Query.GetAcademicPrograms;

namespace Universe.Api.Controllers;

[Route("colleges/{collegeId:guid}/academic-programs")]
[ApiController, Authorize(Roles = "Admin , AcademicAdvising")]
public class AcademicProgramController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAcademicProgramCommand(id) , cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll([FromRoute] Guid collegeId , [FromQuery] FilterRequest filter , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAcademicProgramsCommand(collegeId , filter), cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPost("")]
    public async Task<IActionResult> Add([FromRoute] Guid collegeId , [FromBody] AddAcademicProgramCommand request , CancellationToken cancellationToken)
    {
        request = request with { CollegeId = collegeId };

        var result = await _mediator.Send(request , cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid collegeId , [FromRoute] Guid id, UpdateAcademicProgramCommand request , CancellationToken cancellationToken)
    {
        request = request with { CollegeId = collegeId , Id = id };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RemoveAcademicProgramCommand(id), cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }
}
