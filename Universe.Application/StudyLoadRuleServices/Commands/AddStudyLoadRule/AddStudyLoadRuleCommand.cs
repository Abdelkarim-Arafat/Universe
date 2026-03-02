using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.StudyLoadRuleServices.Dtos;

namespace Universe.Application.StudyLoadRuleServices.Commands.AddStudyLoadRule;

public record AddStudyLoadRuleCommand(
    [Required] Guid AcademicProgramId,
    decimal GpaFrom,
    decimal GpaTo,
    int MinHours,
    int MaxHours
) : IRequest<Result<StudyLoadRuleResponse>>;
