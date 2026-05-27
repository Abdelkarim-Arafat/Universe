namespace Universe.Application.UserServices.Commands.UpdateImage;

public record UpdateImageCommand (
    [Required] string OldImageUrl,
    [Required] IFormFile NewImageFile,
    [Required] Guid UserId
) : IRequest<Result<string>>;
