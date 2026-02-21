using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.BuildingServices.Dtos;

namespace Universe.Application.BuildingServices.Queries.GetAll;

public record GetAllQuery
(

) : IRequest<Result<List<BuildingResponse>>>;
