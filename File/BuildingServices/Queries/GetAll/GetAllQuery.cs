using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.BuildingServices.Queries.GetAll;

public record GetAllQuery
(

) : IRequest<Result<List<BuildingResponse>>>;
