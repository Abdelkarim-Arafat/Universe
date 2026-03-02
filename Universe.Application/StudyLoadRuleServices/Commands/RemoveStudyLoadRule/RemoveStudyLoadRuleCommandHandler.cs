
using Universe.Core.Interfaces;

namespace Universe.Application.StudyLoadRuleServices.Commands.RemoveStudyLoadRule;

internal class RemoveStudyLoadRuleCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<RemoveStudyLoadRuleCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(RemoveStudyLoadRuleCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.StudyLoadRuleRepository
            .GetByIdAsync(request.Id, cancellationToken) is not { } studyLoadRlule
            ) return Result.Failure(StudyLoadRuleErrors.NotFound);

        _unitOfWork.Repository<StudyLoadRule>().DeletePermanently(studyLoadRlule);

        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}
