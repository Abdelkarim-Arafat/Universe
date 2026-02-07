using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.AuthServices.AuthDtos;
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
        if(result.IsFailure) return result.ToProblem();

        SetTokensInCookie(result.Value);

        return Ok(new
        {
            result.Value.Id,
            result.Value.Name,
            result.Value.Email,
            result.Value.Roles,
            result.Value.Permissions
        });
    }

    [HttpDelete("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("access_Token");
        Response.Cookies.Delete("refresh_Token");
        return Ok();
    }

    [HttpPost("update-refresh-token")]
    public async Task<IActionResult> UpdateRefreshToken([FromBody] UpdateRefreshTokenCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.IsFailure) return result.ToProblem();

        SetTokensInCookie(result.Value);

        return Ok(new
        {
            result.Value.Id,
            result.Value.Name,
            result.Value.Email,
            result.Value.Roles,
            result.Value.Permissions
        });
    }

    [HttpDelete("revoke-refresh-token")]
    public async Task<IActionResult> RevokeRefreshToken([FromBody] RevokeRefreshTokenCommand request)
    {
        var result = await _mediator.Send(request);
        if(result.IsFailure) return result.ToProblem();

        Response.Cookies.Delete("access_Token");

        return Ok();
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


    private void SetTokensInCookie(AuthResponse response)
    {
        var accessTokenCookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddSeconds(response.ExpiresIn),
            Secure = true,
            SameSite = SameSiteMode.None
        };
        var refreshTokenCookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = response.RefreshTokenExpiration,
            Secure = true,
            SameSite = SameSiteMode.None
        };

        Response.Cookies.Append("access_Token", response.Token, accessTokenCookieOptions);
        Response.Cookies.Append("refresh_Token", response.RefreshToken, refreshTokenCookieOptions);
    }
}

