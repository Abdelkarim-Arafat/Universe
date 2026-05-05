using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Universe.Application.UserServices.Commands.UploadImage;

internal class UploadImageCommandHandler(
    IHttpContextAccessor httpContext,
    IImageService imageService,
    UserManager<ApplicationUser> userManager
    ) : IRequestHandler<UploadImageCommand, Result<string>>
{
    private readonly IHttpContextAccessor _httpContext = httpContext;
    private readonly IImageService _imageService = imageService;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result<string>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        if (request.File.Length == 0)
            return Result.Failure<string>(new Error("ImageFile.Empty", "File is empty", StatusCodes.Status400BadRequest));

        var claims = _httpContext.HttpContext?.User;

        var userId = claims?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        var user = await _userManager.FindByIdAsync(userId!);

        if (user is null || user.IsDeleted) return Result.Failure<string>(AuthErrors.UserNotFound);

        var imageUrl = await _imageService.UploadAsync(request.File);

        user.ImageUrl = imageUrl;

        await _userManager.UpdateAsync(user);

        return Result.Success(imageUrl);
    }
}
