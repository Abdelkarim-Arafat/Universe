using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Universe.Api.Extensions;
using Universe.Application.UserServices.Commands.ChangePassword;
using Universe.Application.UserServices.Commands.RemoveImage;
using Universe.Application.UserServices.Commands.ResetUserPassword;
using Universe.Application.UserServices.Commands.UpdateEmail;
using Universe.Application.UserServices.Commands.UpdateImage;
using Universe.Application.UserServices.Commands.UploadImage;
using Universe.Core.Constants;

namespace Universe.Api.Controllers;

[Route("users")]
[ApiController, Authorize]
public class UserController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("upload-image")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = $"{Roles.AdminOrAdvisor} , {Roles.Student}")]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile file , CancellationToken cancellationToken)
    {
        var command = new UploadImageCommand(file);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPatch("update-image")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = $"{Roles.AdminOrAdvisor} , {Roles.Student}")]
    public async Task<IActionResult> UpdateImage([FromForm] IFormFile newImageFile, [FromForm] string oldImageUrl, CancellationToken cancellationToken)
    {
        var command = new UpdateImageCommand(oldImageUrl, newImageFile);
        var result = await _mediator.Send(command , cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpDelete("remove-image")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = $"{Roles.AdminOrAdvisor} , {Roles.Student}")]
    public async Task<IActionResult> RemoveImage(string imageUrl, CancellationToken cancellationToken)
    {
        var command = new RemoveImageCommand(imageUrl);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpPatch("change-password")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = $"{Roles.AllRoles}")]
    public async Task<IActionResult> ChangePassword (
    [FromBody] ChangePasswordCommand request,
    CancellationToken cancellationToken)
    {
        request = request with { UserId = Guid.Parse(User.GetUserId()!) };
        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }


    [HttpPatch("reset-password")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = $"{Roles.AllRoles}")]
    public async Task<IActionResult> ResetUserPassword(
    [FromBody] ResetUserPasswordCommand request,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpPatch("update-email")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = $"{Roles.AllRoles}")]
    public async Task<IActionResult> UpdateEmail (
    [FromBody] UpdateEmailCommand request,
    CancellationToken cancellationToken)
    {
        request = request with { UserId = Guid.Parse(User.GetUserId()!) };
        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }
}