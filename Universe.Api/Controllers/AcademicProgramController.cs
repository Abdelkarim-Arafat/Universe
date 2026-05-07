using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Universe.Api.Extensions;
using Universe.Application.AcademicProgramServices.Commands.AddAcademicProgram;
using Universe.Application.AcademicProgramServices.Commands.RemoveAcademicProgram;
using Universe.Application.AcademicProgramServices.Commands.UpdateAcademicProgram;
using Universe.Application.AcademicProgramServices.Query.GetAcademicProgram;
using Universe.Application.AcademicProgramServices.Query.GetAcademicPrograms;
using Universe.Application.Common;
using Universe.Core.Constants;

namespace Universe.Api.Controllers;

[Route("colleges/{collegeId:guid}/academic-programs")]
[ApiController]
public class AcademicProgramController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id:guid}")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> Get(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAcademicProgramQuery(id) , cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisorOrStaff)]
    public async Task<IActionResult> GetAll([FromRoute] Guid collegeId , [FromQuery] FilterRequest filter , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAcademicProgramsQuery(collegeId , filter), cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPost("")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> Add([FromRoute] Guid collegeId , [FromBody] AddAcademicProgramCommand request , CancellationToken cancellationToken)
    {
        request = request with { CollegeId = collegeId };

        var result = await _mediator.Send(request , cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }
    [HttpPut("{id:guid}")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> Update([FromRoute] Guid collegeId , [FromRoute] Guid id, UpdateAcademicProgramCommand request , CancellationToken cancellationToken)
    {
        request = request with { CollegeId = collegeId , Id = id };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpDelete("{id:guid}")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> Delete([FromRoute] Guid id , [FromRoute] Guid collegeId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RemoveAcademicProgramCommand(collegeId, id), cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }
}
