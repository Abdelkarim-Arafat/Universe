using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.StudyLoadByLevelServices.StudyLoadByLevelDtos;
using Universe.Core.Interfaces;

namespace Universe.Application.StudyLoadByLevelServices.Commands.UpdateStudyLoad;

public class UpdateStudyLoadByLevelCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateStudyLoadByLevelCommand, Result<StudyLoadByLevelResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<StudyLoadByLevelResponse>> Handle(UpdateStudyLoadByLevelCommand request, CancellationToken cancellationToken)
    {
        if(await _unitOfWork.StudyLoadByLevelRepository
            .GetByIdAsync(request.Id , cancellationToken) is not { } studyLoad
            ) return Result.Failure<StudyLoadByLevelResponse>(StudyLoadByLevelErrors.NotFound);


        studyLoad.MaxHours = request.MaxHours;
        studyLoad.MinHours = request.MinHours;

        _unitOfWork.Repository<StudyLoadByLevel>().Update(studyLoad);

        await _unitOfWork.CompleteAsync(cancellationToken);

        var response = new StudyLoadByLevelResponse(
            studyLoad.Id.ToString(),
            studyLoad.LevelId.ToString(),
            studyLoad.Level.Name,
            studyLoad.Sememester.Name,
            studyLoad.MinHours,
            studyLoad.MaxHours
        );
        return Result.Success(response);
    }
}