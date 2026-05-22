namespace Universe.Application.StudyLoadRuleServices.Commands.UpdateStudyLoadRule;

public record UpdateStudyLoadRuleCommand(
    [Required] Guid Id,
    [Required] Guid AcademicProgramId,
    decimal GpaFrom,
    decimal GpaTo,
    int MinHours,
    int MaxHours
) : IRequest<Result<StudyLoadRuleResponse>>;
