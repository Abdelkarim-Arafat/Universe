using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AcademicProgramServices.AcademicProgramDtos;

public record AcademicProgramResponse(
    string Id,
    string Name,
    string Code,
    string Description,
    int RequiredCreditHours
);
