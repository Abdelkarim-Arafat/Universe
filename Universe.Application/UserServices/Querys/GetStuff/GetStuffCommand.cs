using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Querys.GetStuff;

public record GetStuffCommand (
    [Required] Guid UserId
) : IRequest<Result<StuffWithDetailsResponse>>;