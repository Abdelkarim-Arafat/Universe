using Universe.Application.LevelServices.LevelDtos;

namespace Universe.Application.LevelServices.Commands.UpdateLevel;

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
            .CheckOverLabedHoursAsync(command.MinHours, command.MaxHours, command.Id, cancellationToken);
        if (isLevelWithOverLabExist)
            return Result.Failure<LevelResponse>(LevelErrors.InvalidHours);



        level.Name = command.Name;
        level.MinHours = command.MinHours;
        level.MaxHours = command.MaxHours;

        _unitOfWork.Repository<Level>().Update(level);

        try
        {
            await _unitOfWork.CompleteAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            return Result.Failure<LevelResponse>(
                new Error("DatabaseError", "Failed to update level", StatusCodes.Status409Conflict));
        }


        return Result.Success(level.Adapt<LevelResponse>());
    }
}
