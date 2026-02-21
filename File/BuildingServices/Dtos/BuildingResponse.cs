using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.BuildingServices.Dtos;

public record BuildingResponse
(
    Guid Id,
    string Name,
    string Code
);
