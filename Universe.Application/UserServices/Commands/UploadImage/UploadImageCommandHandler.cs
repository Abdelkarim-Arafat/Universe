using System.Security.Claims;

namespace Universe.Application.UserServices.Commands.UploadImage;

internal class UploadImageCommandHandler(
    IImageService imageService,
    UserManager<ApplicationUser> userManager
    ) : IRequestHandler<UploadImageCommand, Result<string>>
{
    private readonly IImageService _imageService = imageService;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result<string>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        if (request.File.Length == 0)
            return Result.Failure<string>(new Error("ImageFile.Empty", "File is empty", StatusCodes.Status400BadRequest));

        var user = await _userManager.FindByIdAsync(request.UserId.ToString());

        if (user is null || user.IsDeleted) return Result.Failure<string>(AuthErrors.UserNotFound);

        var imageUrl = await _imageService.UploadAsync(request.File);

        user.ImageUrl = imageUrl;

        await _userManager.UpdateAsync(user);

        return Result.Success(imageUrl);
    }
}
