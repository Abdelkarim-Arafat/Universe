using Universe.Application.LevelServices.LevelDtos;

namespace Universe.Application.LevelServices.Commands.UpdateLevel;

public class UpdateLevelCommandHandler
    (IUnitOfWork unitOfWork) : IRequestHandler<UpdateLevelCommand, Result<LevelResponse>>
{
    private readonly IUnitOfWork _unitofwork = unitOfWork;

    public async Task<Result<LevelResponse>> Handle(UpdateLevelCommand command, CancellationToken cancellationToken)
    {
        var check = await _unitofwork.LevelRepository.CheckOverLabedHoursAsync(command.MinHours, command.MaxHours,command.Id, cancellationToken);
        if (check.IsSuccess)
        {
            var result = await _unitofwork.LevelRepository.GetByIdAsync(command.Id, cancellationToken);
            if (result.IsSuccess)
            {
                var level = result.Value;

                level.Name = command.Name;
                level.MinHours = command.MinHours;
                level.MaxHours = command.MaxHours;

                _unitofwork.Repository<Level>().Update(level, cancellationToken);

                await _unitofwork.CompleteAsync(cancellationToken);
                return Result.Success(level.Adapt<LevelResponse>());
            }
            return Result.Failure<LevelResponse>(result.Error);
        }
        var error = check.Error;
        return Result.Failure<LevelResponse>(error);
    }
}
