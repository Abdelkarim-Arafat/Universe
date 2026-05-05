using Universe.Application.LevelServices.Dtos;

namespace Universe.Application.LevelServices.Commands.Update;

public class UpdateLevelCommandHandler
    (IUnitOfWork unitOfWork) : IRequestHandler<UpdateLevelCommand, Result<LevelResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<LevelResponse>> Handle(UpdateLevelCommand command, CancellationToken cancellationToken)
    {
        var level = await _unitOfWork.LevelRepository.GetByIdAsync(command.Id, cancellationToken);
        if (level is null)
            return Result.Failure<LevelResponse>(LevelErrors.NotFound);

        var isLevelWithOverLabExist = await _unitOfWork.LevelRepository
            .CheckOverLabedHoursAsync(command.MinHours, command.MaxHours, level.Id, level.AcademicProgramId, cancellationToken);
        if (isLevelWithOverLabExist)
            return Result.Failure<LevelResponse>(LevelErrors.InvalidHours);

        level.Name = command.Name;
        level.MinHours = command.MinHours;
        level.MaxHours = command.MaxHours;

        _unitOfWork.Repository<Level>().Update(level);

        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(level.Adapt<LevelResponse>());
    }
}
