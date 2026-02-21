using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.RoomTypeServices.Dtos;

namespace Universe.Application.RoomTypeServices.Queries.GetAllTypes;

public record GetAllTypesQuery
() : IRequest<Result<List<RoomTypeResponse>>>;
