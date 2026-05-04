using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.AcademicProgram;

namespace Universe.Application.AcademicProgramServices.Commands.UpdateAcademicProgram;

public record UpdateAcademicProgramCommand(
    [Required] Guid Id,
    [Required] Guid CollegeId,
    string Name,
    string Description,
    string Code,
    int RequiredCreditHours

) : IRequest<Result<AcademicProgramResponse>>;