using MediatR;

using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.EnrollmentServices.Commands.Update;
using Universe.Application.EnrollmentServices.Queries.GetEnrollmentPage;

namespace Universe.Api.Controllers;

[Route("enrollments")]
[ApiController]
public class EnrollmentController(IMediator mediator) : ControllerBase
{

    private readonly IMediator _mediator = mediator;
    [HttpGet]
    public async Task<IActionResult>
        GetEnrollmentPage([FromQuery] Guid SemesterId, [FromQuery] Guid StudentId, [FromQuery] Guid LevelId, CancellationToken cancellationToken)
    {
        var query = new GetEnrollmentPageQuery(StudentId, SemesterId, LevelId);
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPut]
    public async Task<IActionResult> Update(
        [FromBody] UpdateEnrollmentCommand command,
        [FromQuery] NeededHours Hours,
        [FromQuery] Guid StudentId, CancellationToken cancellationToken)
    {
        command = command with { Hours = Hours, StudentId = StudentId };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}
