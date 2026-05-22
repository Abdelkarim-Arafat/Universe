using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.StudyLoadRule;

namespace Universe.Application.StudyLoadRuleServices.Commands.AddStudyLoadRule;

public record AddStudyLoadRuleCommand(
    [Required] Guid AcademicProgramId,
    decimal GpaFrom,
    decimal GpaTo,
    int MinHours,
    int MaxHours
) : IRequest<Result<StudyLoadRuleResponse>>;
