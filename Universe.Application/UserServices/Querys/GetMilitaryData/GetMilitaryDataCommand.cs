using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Querys.GetMilitaryData;


public record GetMilitaryDataCommand(
    [Required] Guid StudentId
) : IRequest<Result<MilitaryDataResponse>>;