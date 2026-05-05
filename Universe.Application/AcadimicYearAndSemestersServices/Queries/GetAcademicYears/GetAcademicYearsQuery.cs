using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.AcadimicYearAndSemesters;

namespace Universe.Application.AcadimicYearAndSemestersServices.Queries.GetAcademicYears;

public record GetAcademicYearsQuery (
    Guid CollegeId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<AcademicYearResponse>>>;
