using MimeKit.IO;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.StudyLoadByLevel;

namespace Universe.Application.StudyLoadByLevelServices.Queries.GetAll;

public record GetStudyLoadByLevelsQuery(
    [Required] Guid ProgramId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<StudyLoadByLevelResponse>>>;
