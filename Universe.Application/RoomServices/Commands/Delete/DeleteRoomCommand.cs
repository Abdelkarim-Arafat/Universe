using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.RoomServices.Commands.Delete;

public record DeleteRoomCommand
([Required]Guid Id) : IRequest<Result>;
