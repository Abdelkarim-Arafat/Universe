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
using Universe.Application.UserServices.Queries.GetImageUrl;
using Universe.Core.Abstractions;
using Universe.Core.Constants;

namespace Universe.Api.Controllers;

[Route("users")]
[ApiController, Authorize]
public class UserController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private Guid GetUserId() => Guid.Parse(User.GetUserId()!);

    [HttpPost("upload-image")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AllRoles)]
    public async Task<IActionResult> UploadImage(
        [FromForm] IFormFile file,
        [FromQuery] Guid? userId,
        CancellationToken cancellationToken)
    {
        var resolvedId = ResolveUserId(userId);
        if (!resolvedId.IsSuccess) return resolvedId.ToProblem();

        var command = new UploadImageCommand(file, resolvedId.Value);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPatch("update-image")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AllRoles)]
    public async Task<IActionResult> UpdateImage(
        [FromForm] IFormFile newImageFile,
        [FromForm] string oldImageUrl,
        [FromQuery] Guid? userId,
        CancellationToken cancellationToken)
    {
        var resolvedId = ResolveUserId(userId);
        if (!resolvedId.IsSuccess) return resolvedId.ToProblem();

        var command = new UpdateImageCommand(oldImageUrl, newImageFile , resolvedId.Value);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpDelete("remove-image")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AllRoles)]
    public async Task<IActionResult> RemoveImage(
        [FromQuery] Guid? userId,
        [FromQuery] string imageUrl,
        CancellationToken cancellationToken)
    {
        var resolvedId = ResolveUserId(userId);
        if(!resolvedId.IsSuccess) return resolvedId.ToProblem();

        var command = new RemoveImageCommand(resolvedId.Value, imageUrl);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpPatch("change-password")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AllRoles)]
    public async Task<IActionResult> ChangePassword(
    [FromBody] ChangePasswordCommand request,
    CancellationToken cancellationToken)
    {
        request = request with { UserId = GetUserId() };
        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }


    [HttpPatch("reset-password")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AllRoles)]
    public async Task<IActionResult> ResetUserPassword(
    [FromBody] ResetUserPasswordCommand request,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpPatch("update-email")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AllRoles)]
    public async Task<IActionResult> UpdateEmail(
    [FromBody] UpdateEmailCommand request,
    CancellationToken cancellationToken)
    {
        request = request with { UserId = GetUserId() };
        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpGet("get-image-url")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> GetImageUrl([FromQuery]Guid userId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetImageUrlQuery(userId), cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    private Result<Guid> ResolveUserId(Guid? userId)
    {
        if (User.IsInRole(Roles.Student) || User.IsInRole(Roles.Staff))
            return Result.Success(GetUserId());

        if (userId is null)
            return Result.Failure<Guid>(new Error(
                code: "User.IdRequired",
                message: "UserId is required for admin operations.",
                statusCode: StatusCodes.Status400BadRequest
            ));
        return Result.Success(userId.Value);
    }

}