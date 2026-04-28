using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.RoleServices.Queries;

namespace Universe.Api.Controllers;

[Route("roles")]
[ApiController , Authorize]
public class RoleController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAllRoles(
        [FromQuery] string roleName,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllRolesCommand(roleName), cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }
}
