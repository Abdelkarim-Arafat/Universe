using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.AcademicYearAndSemesters;

namespace Universe.Application.AcadimicYearAndSemestersServices.Queries.GetResultAnnouncementStatus;


public record GetResultAnnouncementStatusQuery(
    [Required] Guid SemesterId
) : IRequest<Result<ResultAnnounceStatusResponse>>;
