using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.StudyLoadByLevelServices.StudyLoadByLevelDtos;
using Universe.Application.StudyLoadRuleServices.Dtos;

namespace Universe.Application.StudyLoadByLevelServices.Commands.AddStudyLoad;

public class AddStudyLoadByLevelCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<AddStudyLoadByLevelCommand , Result<StudyLoadByLevelResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<StudyLoadByLevelResponse>> Handle(AddStudyLoadByLevelCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.AcademicYearRepository
            .GetSemesterByTypeAsync(request.AcademicYearId, request.SemesterType, cancellationToken) is not { } semester
            ) return Result.Failure<StudyLoadByLevelResponse>(SemesterErrors.NotFound);

        if (await _unitOfWork.StudyLoadByLevelRepository
            .IsExistAsync(request.ProgramId, request.LevelId, semester.Id, cancellationToken)
            ) return Result.Failure<StudyLoadByLevelResponse>(StudyLoadByLevelErrors.AlreadyExist);

        if (await _unitOfWork.LevelRepository
            .GetByIdAsync(request.LevelId, cancellationToken) is not { } level
            ) return Result.Failure<StudyLoadByLevelResponse>(LevelErrors.NotFound);


        var studyLoad = new StudyLoadByLevel
        {
            AcademicProgramId = request.ProgramId,
            LevelId = request.LevelId,
            SemesterId = semester.Id,
            MinHours = request.MinHours,
            MaxHours = request.MaxHours,
        };


        await _unitOfWork.Repository<StudyLoadByLevel>().AddAsync(studyLoad, cancellationToken);

        await _unitOfWork.CompleteAsync(cancellationToken);

        var response = new StudyLoadByLevelResponse(
            studyLoad.Id.ToString(),
            level.Id.ToString(),
            level.Name,
            request.SemesterType,
            studyLoad.MinHours,
            studyLoad.MaxHours
        );

        return Result.Success(response);
    }
}