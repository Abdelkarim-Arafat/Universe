using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Querys.GetAllStudents;

public record GetProgramStudentsQuery(
    [Required] Guid ProgramId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<StudentResponse>>>;