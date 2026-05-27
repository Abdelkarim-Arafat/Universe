namespace Universe.Application.UserServices.Commands.RemoveImage;

public record RemoveImageCommand(
    [Required] Guid UserId,
    [Required] string ImageUrl
) : IRequest<Result>;