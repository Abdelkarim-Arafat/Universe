namespace Universe.Application.LevelServices.Commands.RemoveLevel;

public class RemoveLevelCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RemoveLevelCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(RemoveLevelCommand command, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.LevelRepository.GetByIdAsync(command.Id, cancellationToken);
        if (result.IsSuccess)
        {
            Level level = result.Value;
            _unitOfWork.Repository<Level>().SoftDelete(level, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
        }
        return Result.Success();
    }
}