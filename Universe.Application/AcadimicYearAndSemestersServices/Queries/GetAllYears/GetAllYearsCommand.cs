using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicYearAndSemestersServices.Dtos;
using Universe.Application.AcadimicYearAndSemestersServices.Dtos;

namespace Universe.Application.AcadimicYearAndSemestersServices.Queries.GetAllYears;

public record GetAllYearsCommand (
    Guid CollegeId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<CurrentAcademicYearResponse>>>;
