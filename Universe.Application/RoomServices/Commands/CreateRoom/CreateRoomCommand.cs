using Universe.Application.RoomServices.Dtos;

namespace Universe.Application.RoomServices.Commands.CreateRoom;

public record CreateRoomCommand
(
  string Name,
  int RoomNumber,
  int Capacity,
  Guid RoomTypeId,
  Guid BuildingId
) : IRequest<Result<RoomResponse>>;
