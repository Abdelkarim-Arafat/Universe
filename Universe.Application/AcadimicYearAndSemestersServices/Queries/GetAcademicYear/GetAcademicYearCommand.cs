using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicYearAndSemestersServices.Dtos;

namespace Universe.Application.AcadimicYearAndSemestersServices.Queries.GetAcademicYear;

public record GetAcademicYearCommand(
    [Required] Guid Id
) : IRequest<Result<AcademicYearResponse>>;