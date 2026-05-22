namespace Universe.Application.StudyLoadRuleServices.Query.GetAllStudyLoadRule;

public record GetAllStudyLoadRuleCommand(
    [Required] Guid CollegeId
) : IRequest<Result<List<StudyLoadRuleResponse>>>;
