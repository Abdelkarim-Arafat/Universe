using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.StudyLoadByLevelServices.StudyLoadByLevelDtos;
using Universe.Core.Enums;

namespace Universe.Application.StudyLoadByLevelServices.Commands.UpdateStudyLoad;

public record UpdateStudyLoadByLevelCommand(
    [Required] Guid Id,
    int MinHours,
    int MaxHours
) : IRequest<Result<StudyLoadByLevelResponse>>;