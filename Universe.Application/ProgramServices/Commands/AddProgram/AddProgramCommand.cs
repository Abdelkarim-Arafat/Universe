using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicProgramServices.AcademicProgramDtos;

namespace Universe.Application.AcademicProgramServices.Commands.AddAcademicProgram;

public record AddAcademicProgramCommand(
    Guid CollegeId,
    string Name,
    string Code,
    string? Description,
    int RequiredCreditHours
) : IRequest<Result<AcademicProgramResponse>>;