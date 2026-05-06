
using Universe.Core.Contracts.Rooms;
using Universe.Core.Enums;

namespace Universe.Application.RoomServices.Commands.Create;

public record CreateRoomCommand
(
  string Name,
  int RoomNumber,
  int Capacity,
  RoomType RoomType,
  Guid BuildingId
) : IRequest<Result<RoomResponse>>;
