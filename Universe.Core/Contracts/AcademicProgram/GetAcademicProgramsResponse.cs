using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Contracts.AcademicProgram;

public record GetAcademicProgramsResponse (
    Guid Id,
    string Name,
    string Code
);
