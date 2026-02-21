using Universe.Application.RoomTypeServices.Dtos;

namespace Universe.Application.RoomTypeServices.Commands.UpdateRoomType;

public record UpdateRoomTypeCommand(
    [Required]Guid Id,
    string Name
) : IRequest<Result<RoomTypeResponse>>;
