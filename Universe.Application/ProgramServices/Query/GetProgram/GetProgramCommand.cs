using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicProgramServices;
using Universe.Application.AcademicProgramServices.AcademicProgramDtos;

namespace Universe.Application.AcademicProgramServices.Query.GetAcademicProgram;

public record GetAcademicProgramCommand(
    [Required] Guid Id
) : IRequest<Result<AcademicProgramResponse>>;
