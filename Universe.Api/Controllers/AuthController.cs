using MediatR;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.AuthServices.Commands.Login;
using Universe.Application.AuthServices.Commands.Register;
using Universe.Application.AuthServices.Commands.ResetPassword;
using Universe.Application.AuthServices.Commands.RevokeRefreshToken;
using Universe.Application.AuthServices.Commands.SendResetPasswordCodeAsync;
using Universe.Application.AuthServices.Commands.UpdateRefreshToken;

namespace Universe.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    //[HttpPost("register")]
    //public async Task<IActionResult> Register([FromBody] RegisterCommand request)
    //{
    //    var result = await _mediator.Send(request);
    //    return result.IsSuccess ? Ok()
    //        : result.ToProblem();
    //}

    [HttpGet("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok(result.Value)
            : result.ToProblem();
    }
    [HttpPut("update-refresh-token")]
    public async Task<IActionResult> Login([FromBody] UpdateRefreshTokenCommand request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPut("revoke-refresh-token")]
    public async Task<IActionResult> RevokeRefreshToken([FromBody] RevokeRefreshTokenCommand request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok()
            : result.ToProblem();
    }

    [HttpPost("send-reset-password")]
    public async Task<IActionResult> SendResetPasswordConfirmation([FromBody] SendResetPasswordConfirmationCommand request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok()
            : result.ToProblem();
    }

    [HttpPut("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok()
            : result.ToProblem();
    }
}

