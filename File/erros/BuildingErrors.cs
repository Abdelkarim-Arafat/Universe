using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public class BuildingErrors
{
    public static readonly Error NotFound =
      new("Building.NotFound", "No Building with specific id", StatusCodes.Status404NotFound);
    public static readonly Error RoomsFounded =
      new("Building.RoomsFounded", "Cannot remove this building because there is rooms inside it", StatusCodes.Status409Conflict);
}
