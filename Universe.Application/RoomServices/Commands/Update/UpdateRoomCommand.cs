using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.Rooms;
using Universe.Core.Enums;

namespace Universe.Application.RoomServices.Commands.Update;

public record UpdateRoomCommand
(
  string Name,
  int RoomNumber,
  int Capacity,
  Guid Id,
  RoomType RoomType
) : IRequest<Result<RoomResponse>>;
