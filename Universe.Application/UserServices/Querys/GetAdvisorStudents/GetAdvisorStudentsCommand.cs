using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Querys.GetAdvisorStudents;

public record GetAdvisorStudentsCommand (
    [Required] Guid AdvisorId,
    FilterRequest filter
) : IRequest<Result<PaginationList<StudentResponse>>>;