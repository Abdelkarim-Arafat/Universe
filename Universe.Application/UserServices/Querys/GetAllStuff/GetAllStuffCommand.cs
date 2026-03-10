using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AuthServices.AuthDtos;

namespace Universe.Application.UserServices.Querys.GetAllStuff;

public record GetAllStuffCommand(
    [Required] Guid CollegeId,
    FilterRequest filter
) : IRequest<Result<PaginationList<StaffResponse>>>;
