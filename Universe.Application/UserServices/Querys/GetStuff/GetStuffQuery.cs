using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Querys.GetStuff;

public record GetStuffQuery (
    [Required] Guid UserId
) : IRequest<Result<StuffWithDetailsResponse>>;