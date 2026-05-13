using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Querys.GetPersonalData;

public record GetPersonalDataQuery (
    [Required] Guid StudentId
) : IRequest<Result<PersonalDataResponse>>;