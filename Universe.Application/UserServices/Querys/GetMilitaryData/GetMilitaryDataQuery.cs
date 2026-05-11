using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Querys.GetMilitaryData;


public record GetMilitaryDataQuery(
    [Required] Guid StudentId
) : IRequest<Result<MilitaryDataResponse>>;