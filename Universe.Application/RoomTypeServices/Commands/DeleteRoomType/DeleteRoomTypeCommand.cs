namespace Universe.Application.RoomTypeServices.Commands.DeleteRoomType;

public record DeleteRoomTypeCommand
(
    [Required]Guid Id
) : IRequest<Result>;
