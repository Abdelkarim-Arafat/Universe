using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.StudyLoadByLevelServices.StudyLoadByLevelDtos;

namespace Universe.Application.StudyLoadByLevelServices.Queries.GetAll;

public record GetAllStudyLoadByLevelCommand(
    [Required] Guid ProgramId
) : IRequest<Result<List<StudyLoadByLevelResponse>>>;
