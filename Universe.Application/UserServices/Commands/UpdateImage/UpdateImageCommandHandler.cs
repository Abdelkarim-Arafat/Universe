
using System.Security.Claims;

namespace Universe.Application.UserServices.Commands.UpdateImage;

internal class UpdateImageCommandHandler(
    IHttpContextAccessor httpContext,
    IImageService imageService,
    UserManager<ApplicationUser> userManager
    ) : IRequestHandler<UpdateImageCommand, Result<string>>
{
    private readonly IHttpContextAccessor _httpContext = httpContext;
    private readonly IImageService _imageService = imageService;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result<string>> Handle(UpdateImageCommand request, CancellationToken cancellationToken)
    {
        if (request.NewImageFile.Length == 0)
            return Result.Failure<string>(FileErrors.EmptyFile);

        var claims = _httpContext.HttpContext?.User;

        var userId = claims?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var user = await _userManager.FindByIdAsync(userId!);

        if (user is null || user.IsDeleted) return Result.Failure<string>(AuthErrors.UserNotFound);

        if(user.ImageUrl != request.OldImageUrl)
            return Result.Failure<string> (FileErrors.ImageUrlMismatch);

        var imageUrl = await _imageService.UpdateAsync(request.OldImageUrl, request.NewImageFile);

        user.ImageUrl = imageUrl;

        await _userManager.UpdateAsync(user);

        return Result.Success(imageUrl);
    }
}
