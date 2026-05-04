using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Core.Contracts.AcademicProgram;

public record AcademicProgramResponse (
    Guid Id,
    string Name,
    string Code,
    string Description,
    int RequiredCreditHours,
    AcademicLoad? AcademicLoad,
    AcademicDegree? AcademicDegree,
    string CertificateTitle
);
