using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.AcademicProgram;

namespace Universe.Application.AcademicProgramServices.Commands.AddAcademicProgram;

public record AddAcademicProgramCommand (
    [Required] Guid CollegeId,
    string Name,
    string Code,
    string? Description,
    int RequiredCreditHours
) : IRequest<Result<AcademicProgramResponse>>;