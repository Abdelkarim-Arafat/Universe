using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.StudyLoadByLevel;
using Universe.Core.Interfaces;

namespace Universe.Application.StudyLoadByLevelServices.Commands.UpdateStudyLoad;

public class UpdateStudyLoadByLevelCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<UpdateStudyLoadByLevelCommand, Result<StudyLoadByLevelResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<StudyLoadByLevelResponse>> Handle(UpdateStudyLoadByLevelCommand request, CancellationToken cancellationToken)
    {
        if(await _unitOfWork.StudyLoadByLevelRepository
            .GetByIdAsync(request.Id , cancellationToken) is not { } studyLoad
            ) return Result.Failure<StudyLoadByLevelResponse>(StudyLoadByLevelErrors.NotFound);

        studyLoad.MaxHours = request.MaxHours;
        studyLoad.MinHours = request.MinHours;

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(StudyLoadByLevelCacheKeys.Tags(request.ProgramId) , cancellationToken);

        var response = new StudyLoadByLevelResponse(
            studyLoad.Id,
            studyLoad.SemesterType,
            studyLoad.Level.Name,
            studyLoad.LevelId,
            studyLoad.MinHours,
            studyLoad.MaxHours
        );
        return Result.Success(response);
    }
}