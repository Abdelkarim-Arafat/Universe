namespace Universe.Application.LevelServices.Commands.Remove;

public class RemoveLevelCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RemoveLevelCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(RemoveLevelCommand command, CancellationToken cancellationToken)
    {
        var level = await _unitOfWork.LevelRepository.GetByIdAsync(command.Id, cancellationToken);
        if (level is null)
            return Result.Failure(LevelErrors.NotFound);

        _unitOfWork.Repository<Level>().DeletePermanently(level);

        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}