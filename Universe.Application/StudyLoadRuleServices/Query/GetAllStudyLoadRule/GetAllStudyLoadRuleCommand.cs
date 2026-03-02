using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.StudyLoadRuleServices.Dtos;

namespace Universe.Application.StudyLoadRuleServices.Query.GetAllStudyLoadRule;

public record GetAllStudyLoadRuleCommand(
    [Required] Guid CollegeId
) : IRequest<Result<List<StudyLoadRuleResponse>>>;
