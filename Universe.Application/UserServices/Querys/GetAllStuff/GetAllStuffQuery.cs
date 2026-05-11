using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AuthServices.AuthDtos;
using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Querys.GetAllStuff;

public record GetAllStuffQuery(
    [Required] Guid CollegeId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<StuffResponse>>>;
