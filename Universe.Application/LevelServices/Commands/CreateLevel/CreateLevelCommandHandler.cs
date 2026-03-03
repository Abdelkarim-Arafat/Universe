
using Universe.Application.LevelServices.LevelDtos;
using Universe.Application.RoomServices.Dtos;

namespace Universe.Application.LevelServices.Commands.CreateLevel;

public class CreateLevelCommandHandler
    (IUnitOfWork unitOfWork, ILogger<CreateLevelCommandHandler> logger) : IRequestHandler<CreateLevelCommand, Result<LevelResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<CreateLevelCommandHandler> _logger = logger;

    public async Task<Result<LevelResponse>> Handle(CreateLevelCommand command, CancellationToken cancellationToken)
    {
        var isProgramExist = await _unitOfWork.AcademicProgramRepository.IsExistAsync(command.AcademicProgramId, cancellationToken);
        if (!isProgramExist)
            return Result.Failure<LevelResponse>(AcademicProgramErrors.AcademicProgramNotFound);

        // 
        var isLevelWithOverLabExist = await _unitOfWork.LevelRepository
            .CheckOverLabedHoursAsync(command.MinHours, command.MaxHours, command.AcademicProgramId, cancellationToken);
        if (isLevelWithOverLabExist)
        {
            return Result.Failure<LevelResponse>(LevelErrors.InvalidHours);
        }

        var level = command.Adapt<Level>();

        await _unitOfWork.Repository<Level>().AddAsync(level , cancellationToken);
        try
        {
            await _unitOfWork.CompleteAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            

            return Result.Failure<LevelResponse>(
                new Error("DatabaseError", "Failed to create level", StatusCodes.Status409Conflict));
        }

        return Result.Success(level.Adapt<LevelResponse>());
    }
}
