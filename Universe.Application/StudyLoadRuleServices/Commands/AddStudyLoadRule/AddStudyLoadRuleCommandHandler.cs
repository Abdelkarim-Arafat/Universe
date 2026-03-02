using Universe.Application.StudyLoadRuleServices.Dtos;
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.StudyLoadRuleServices.Commands.AddStudyLoadRule;

public class AddStudyLoadRuleCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<AddStudyLoadRuleCommand, Result<StudyLoadRuleResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<StudyLoadRuleResponse>> Handle(AddStudyLoadRuleCommand request, CancellationToken cancellationToken)
    {
        if((await _unitOfWork.StudyLoadRuleRepository
            .CheckOverLabedGpaAsync(request.AcademicProgramId, default, request.GpaFrom, request.GpaTo , cancellationToken)))
                return Result.Failure<StudyLoadRuleResponse>(StudyLoadRuleErrors.OverLabedExist);

        var studyLoadRule = request.Adapt<StudyLoadRule>();

        await _unitOfWork.Repository<StudyLoadRule>().AddAsync(studyLoadRule, cancellationToken);

        await _unitOfWork.CompleteAsync(cancellationToken);
        return Result.Success(studyLoadRule.Adapt<StudyLoadRuleResponse>());
    }
}
