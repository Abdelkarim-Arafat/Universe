using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Querys.GetAllStudents;

public record GetAllStudentsCommand(
    [Required] Guid CollegeId,
    FilterRequest filter
) : IRequest<Result<PaginationList<StudentResponse>>>;