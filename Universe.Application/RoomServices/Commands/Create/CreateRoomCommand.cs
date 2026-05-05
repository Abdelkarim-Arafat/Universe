using Universe.Application.RoomServices.Dtos;
using Universe.Core.Enums;

namespace Universe.Application.RoomServices.Commands.Create;

public record CreateRoomCommand
(
  string Name,
  int RoomNumber,
  int Capacity,
  RoomType RoomType,
  [Required] Guid BuildingId
) : IRequest<Result<RoomResponse>>;
