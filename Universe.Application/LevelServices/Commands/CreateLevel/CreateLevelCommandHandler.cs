using Universe.Application.LevelServices.LevelDtos;
namespace Universe.Application.LevelServices.Commands.CreateLevel;

public class CreateLevelCommandHandler
    (IUnitOfWork unitOfWork) : IRequestHandler<CreateLevelCommand, Result<LevelResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<LevelResponse>> Handle(CreateLevelCommand command, CancellationToken cancellationToken)
    {
        var isProgramExist = await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(command.AcademicProgramId, cancellationToken);

        if (!isProgramExist)
            return Result.Failure<LevelResponse>(AcademicProgramErrors.AcademicProgramNotFound);


        var isLevelWithOverLabExist = await _unitOfWork.LevelRepository
            .CheckOverLabedHoursAsync(command.MinHours, command.MaxHours, command.AcademicProgramId, cancellationToken);

        if (isLevelWithOverLabExist)
            return Result.Failure<LevelResponse>(LevelErrors.InvalidHours);


        var level = command.Adapt<Level>();

        await _unitOfWork.Repository<Level>().AddAsync(level, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(level.Adapt<LevelResponse>());
    }
}
