namespace Universe.Application.UserServices.Commands.RemoveImage;

public record RemoveImageCommand(
    string ImageUrl
) : IRequest<Result>;