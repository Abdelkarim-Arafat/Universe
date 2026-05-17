using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Querys.GetStudentGraduationDetails;

public record GetStudentGraduationDetailsQuery(
    [Required] Guid StudentId,
    [Required] Guid ProgramId
) : IRequest<Result<GraduationDetailsResponse>>;