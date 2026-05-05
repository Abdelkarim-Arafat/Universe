using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.AcadimicYearAndSemesters;

namespace Universe.Application.AcadimicYearAndSemestersServices.Queries.GetCurrentSemester;

public record GetCurrentSemesterQuery(
    [Required] Guid AcademicYearId
) : IRequest<Result<SemesterResponse>>;
