using MediatR;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.ControlServices.Commands.UpsertStudentsDegrees;
using Universe.Application.ControlServices.Queries.GetStudents;
 

namespace Universe.Api.Controllers;

[Route("control")]
[ApiController]
public class ControlController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{AcademicProgramId:guid}")]
    public async Task<IActionResult> GetStudents([FromQuery] GetStudentsCommand request,
        Guid AcademicProgramId, CancellationToken cancellationToken)
    {
        request = request with { AcademicProgramId = AcademicProgramId };
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }
    [HttpPut("{AcademicProgramId:guid}")]
    public async Task<IActionResult> UpsertStudentsDegree(
       Guid AcademicProgramId,
       [FromQuery] Guid CourseOfferingId,
       [FromBody] UpsertStudentsDegreesCommand command,  
       CancellationToken cancellationToken)
    {
         
        var updatedRequest = command with { AcademicProgramId = AcademicProgramId, CourseOfferingId = CourseOfferingId };

        var result = await _mediator.Send(updatedRequest, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }
}
