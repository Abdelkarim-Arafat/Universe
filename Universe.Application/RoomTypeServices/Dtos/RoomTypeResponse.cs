using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.RoomTypeServices.Dtos;

public record RoomTypeResponse
(
    Guid Id,
    string Name
);
