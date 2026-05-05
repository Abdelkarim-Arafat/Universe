using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.StudyLoadByLevelServices.Commands.RemoveStudyLoad;

internal class RemoveStudyLoadByLevelCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<RemoveStudyLoadByLevelCommand , Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result> Handle(RemoveStudyLoadByLevelCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.StudyLoadByLevelRepository
            .GetByIdAsync(request.Id, cancellationToken) is not { } studyLoad
            ) return Result.Failure(StudyLoadRuleErrors.NotFound);

        _unitOfWork.Repository<StudyLoadByLevel>().DeletePermanently(studyLoad);

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(StudyLoadByLevelCacheKeys.Tags(request.ProgramId) , cancellationToken);

        return Result.Success();
    }
}
