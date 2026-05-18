using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Querys.GetStudentsWithoutAdvisor;

public record GetStudentsWithoutAdvisorQuery (
    [Required] Guid ProgramId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<StudentResponse>>>;