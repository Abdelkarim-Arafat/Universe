using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcadimicYearAndSemestersServices.Dtos;

namespace Universe.Application.AcadimicYearAndSemestersServices.Queries.GetCurrentYear;

public record GetCurrentYearCommand(
    [Required] Guid CollegeId
) : IRequest<Result<CurrentAcademicYearResponse>>;