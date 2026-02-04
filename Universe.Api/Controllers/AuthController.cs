using MediatR;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.AuthServices.Commands.Register;
using Universe.Application.AuthServices.Queries.Login;

namespace Universe.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok()
            : result.ToProblem();
    }

    [HttpGet("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok(result.Value)
            : result.ToProblem();
    }
}
