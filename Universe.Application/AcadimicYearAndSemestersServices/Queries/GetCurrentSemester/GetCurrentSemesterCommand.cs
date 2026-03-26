using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcadimicYearAndSemestersServices.Dtos;

namespace Universe.Application.AcadimicYearAndSemestersServices.Queries.GetCurrentSemester;

public record GetCurrentSemesterCommand(
    [Required] Guid AcademicYearId
) : IRequest<Result<SemesterResponse>>;
