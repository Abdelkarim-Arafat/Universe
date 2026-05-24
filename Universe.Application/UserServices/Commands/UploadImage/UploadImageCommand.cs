namespace Universe.Application.UserServices.Commands.UploadImage;

public record UploadImageCommand(
    [Required] IFormFile File
) : IRequest<Result<string>>;