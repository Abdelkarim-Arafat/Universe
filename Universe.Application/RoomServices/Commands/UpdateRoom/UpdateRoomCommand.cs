using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.RoomServices.Dtos;

namespace Universe.Application.RoomServices.Commands.UpdateRoom;

public record UpdateRoomCommand
(
  string Name,
  int RoomNumber,
  int Capacity,
  [Required]Guid Id,
  [Required]Guid RoomTypeId
) : IRequest<Result<RoomResponse>>;
