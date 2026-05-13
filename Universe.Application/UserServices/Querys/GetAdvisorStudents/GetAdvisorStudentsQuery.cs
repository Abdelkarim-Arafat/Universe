using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Querys.GetAdvisorStudents;

public record GetAdvisorStudentsQuery(
    [Required] Guid AdvisorId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<StudentResponse>>>;