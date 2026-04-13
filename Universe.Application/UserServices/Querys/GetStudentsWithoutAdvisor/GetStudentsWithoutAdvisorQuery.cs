using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Querys.GetStudentsWithoutAdvisor;

public record GetStudentsWithoutAdvisorQuery(
    [Required] Guid CollegeId,
    FilterRequest filter
) : IRequest<Result<PaginationList<StudentResponse>>>;