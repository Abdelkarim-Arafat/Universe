using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.AuthServices.AuthDtos;
using Universe.Application.AuthServices.Commands.Login;
using Universe.Application.AuthServices.Commands.ResetPassword;
using Universe.Application.AuthServices.Commands.RevokeRefreshToken;
using Universe.Application.AuthServices.Commands.SendResetPasswordCodeAsync;
using Universe.Application.AuthServices.Commands.UpdateRefreshToken;
using Universe.Application.AuthServices.Commands.VerificationResetPasswordCode;
using Universe.Application.UserServices.Commands.ChangePassword;

namespace Universe.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private const string AccessToken = "access_token";
    private const string RefreshToken = "refresh_token";

    //[HttpPost("register")]
    //public async Task<IActionResult> Register([FromBody] RegisterCommand request)
    //{
    //    var result = await _mediator.Send(request);
    //    return result.IsSuccess ? Ok()
    //        : result.ToProblem();
    //}

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand request)
    {
        var result = await _mediator.Send(request);
        if(result.IsFailure) return result.ToProblem();

        SetTokensInCookie(result.Value);

        return Ok(result.Value);
    }

    [HttpPost("update-refresh-token")]
    public async Task<IActionResult> UpdateRefreshToken()
    {
        if (!Request.Cookies.TryGetValue(RefreshToken, out var refreshToken))
            return Unauthorized();

        var result = await _mediator.Send(new UpdateRefreshTokenCommand(refreshToken));
        if (result.IsFailure) return result.ToProblem();

        SetTokensInCookie(result.Value);

        return Ok(new
        {
            result.Value.Id,
            result.Value.CollegeId,
            result.Value.Name,
            result.Value.Email,
            result.Value.Roles,
            result.Value.Permissions
        });
    }

    [HttpDelete("revoke-refresh-token")]
    public async Task<IActionResult> RevokeRefreshToken()
    {
        if (!Request.Cookies.TryGetValue(RefreshToken, out var refreshToken))
            return Unauthorized();

        var result = await _mediator.Send(new RevokeRefreshTokenCommand(refreshToken));
        if(result.IsFailure) return result.ToProblem();

        Response.Cookies.Delete(AccessToken);
        Response.Cookies.Delete(RefreshToken);

        return Ok();
    }
    [HttpPost("send-reset-password")]
    public async Task<IActionResult> SendResetPasswordConfirmation([FromBody] SendResetPasswordConfirmationCommand request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok()
            : result.ToProblem();
    }

    [HttpPost("verification-reset-password-code")]
    public async Task<IActionResult> VerifyResetPasswordCode([FromBody] VerificationResetPasswordCodeCommand request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok()
            : result.ToProblem();
    }

    [HttpPatch("reset-password")]
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

        Response.Cookies.Append(AccessToken, response.Token, accessTokenCookieOptions);
        Response.Cookies.Append(RefreshToken, response.RefreshToken, refreshTokenCookieOptions);
    }
}