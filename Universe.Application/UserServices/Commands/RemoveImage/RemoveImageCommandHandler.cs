using System.Security.Claims;

namespace Universe.Application.UserServices.Commands.RemoveImage;


internal class RemoveImageCommandHandler(
    IImageService imageService,
    UserManager<ApplicationUser> userManager
    ) : IRequestHandler<RemoveImageCommand, Result>
{
    private readonly IImageService _imageService = imageService;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result> Handle(RemoveImageCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());

        if (user is null || user.IsDeleted) return Result.Failure<string>(AuthErrors.UserNotFound);

        if (user.ImageUrl != request.ImageUrl)
            return Result.Failure<string>(FileErrors.ImageUrlMismatch);

        var imageUrl = await _imageService.DeleteAsync(request.ImageUrl);

        user.ImageUrl = null;

        await _userManager.UpdateAsync(user);

        return Result.Success();
    }
}