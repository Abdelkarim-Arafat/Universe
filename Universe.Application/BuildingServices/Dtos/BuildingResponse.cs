using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.BuildingServices.Dtos;

public record BuildingResponse
(
    string Id,
    string Name,
    string Code
);
