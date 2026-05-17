using Universe.Core.Contracts.StudyLoadByLevel;


namespace Universe.Application.StudyLoadByLevelServices.Commands.AddStudyLoad;

public class AddStudyLoadByLevelCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<AddStudyLoadByLevelCommand , Result<StudyLoadByLevelResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<StudyLoadByLevelResponse>> Handle(AddStudyLoadByLevelCommand request, CancellationToken cancellationToken)
    {

        var level = await _unitOfWork.LevelRepository
            .GetByIdAsync(request.LevelId, cancellationToken);

        if (level == null)
            return Result.Failure<StudyLoadByLevelResponse>(LevelErrors.NotFound);

        var isStudyLoadLevelExist = await _unitOfWork.StudyLoadByLevelRepository
            .IsExistAsync(request.ProgramId, request.LevelId, request.SemesterType, cancellationToken);

        if (isStudyLoadLevelExist)
            return Result.Failure<StudyLoadByLevelResponse>(StudyLoadByLevelErrors.AlreadyExist);

        var studyLoad = new StudyLoadByLevel
        {
            AcademicProgramId = request.ProgramId,
            LevelId = request.LevelId,
            SemesterType = request.SemesterType,
            MinHours = request.MinHours,
            MaxHours = request.MaxHours,
        };

        await _unitOfWork.Repository<StudyLoadByLevel>().AddAsync(studyLoad, cancellationToken);

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(StudyLoadByLevelCacheKeys.Tags(request.ProgramId), cancellationToken);

        var response = new StudyLoadByLevelResponse(
            studyLoad.Id,
            request.SemesterType,
            level.Name,
            level.Id,
            studyLoad.MinHours,
            studyLoad.MaxHours
        );
        return Result.Success(response);
    }
}