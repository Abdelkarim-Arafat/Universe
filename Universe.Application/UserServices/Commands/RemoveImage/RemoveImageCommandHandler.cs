using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Universe.Application.UserServices.Commands.UpdateImage;

namespace Universe.Application.UserServices.Commands.RemoveImage;


internal class RemoveImageCommandHandler(
    IHttpContextAccessor httpContext,
    IImageService imageService,
    UserManager<ApplicationUser> userManager
    ) : IRequestHandler<RemoveImageCommand, Result>
{
    private readonly IHttpContextAccessor _httpContext = httpContext;
    private readonly IImageService _imageService = imageService;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result> Handle(RemoveImageCommand request, CancellationToken cancellationToken)
    {
        var claims = _httpContext.HttpContext?.User;

        var userId = claims?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var user = await _userManager.FindByIdAsync(userId!);

        if (user is null || user.IsDeleted) return Result.Failure<string>(AuthErrors.UserNotFound);

        if (user.ImageUrl != request.ImageUrl)
            return Result.Failure<string>(FileErrors.ImageUrlMismatch);

        var imageUrl = await _imageService.DeleteAsync(request.ImageUrl);

        user.ImageUrl = null;

        await _userManager.UpdateAsync(user);

        return Result.Success();
    }
}