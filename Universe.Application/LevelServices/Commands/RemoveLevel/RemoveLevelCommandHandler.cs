using Universe.Application.LevelServices.LevelDtos;

namespace Universe.Application.LevelServices.Commands.RemoveLevel;

public class RemoveLevelCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RemoveLevelCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(RemoveLevelCommand command, CancellationToken cancellationToken)
    {
        var level = await _unitOfWork.LevelRepository.GetByIdAsync(command.Id, cancellationToken);
        if (level is null)
            return Result.Failure(LevelErrors.NotFound);

        _unitOfWork.Repository<Level>().SoftDelete(level);
        _unitOfWork.Repository<Level>().Update(level);

        try
        {
            await _unitOfWork.CompleteAsync(cancellationToken);
        }
        catch (DbUpdateException )
        {
            return Result.Failure(
                new Error("DatabaseError", "Failed to remove level", StatusCodes.Status409Conflict));
        }
        return Result.Success();
    }
}