namespace Universe.Application.UserServices.Commands.UploadImage;

public record UploadImageCommand(
    [Required] IFormFile File,
    [Required] Guid UserId
) : IRequest<Result<string>>;