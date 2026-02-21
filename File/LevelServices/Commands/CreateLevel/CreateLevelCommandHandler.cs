
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
        _logger.LogInformation
            ("Creating Level started. MinHours={MinHours}, MaxHours={MaxHours}",command.MinHours, command.MaxHours);

        var result = await _unitOfWork.LevelRepository.CheckOverLabedHoursAsync(command.MinHours, command.MaxHours, cancellationToken);
        if (result.IsFailure)
        {
            _logger.LogWarning(
        "Level creation failed due to overlapped hours. MinHours={MinHours}, MaxHours={MaxHours}, Error={Error}",
        command.MinHours,
        command.MaxHours,
        result.Error.Code);

            return Result.Failure<LevelResponse>(result.Error);
        }

        var level = command.Adapt<Level>();

        _logger.LogDebug(
    "Level entity mapped successfully from command. Level={@Level}",
    level);

        _unitOfWork.Repository<Level>().Add(level);
        try
        {
            await _unitOfWork.CompleteAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(
                ex,
                "Database error while creating Level. MinHours={MinHours}, MaxHours={MaxHours}",
                command.MinHours,
                command.MaxHours);

            return Result.Failure<LevelResponse>(
                new Error("DatabaseError", "Failed to create level", StatusCodes.Status409Conflict));
        }

        _logger.LogInformation(
    "Level created successfully. LevelId={LevelId}",
    level.Id);

        return Result.Success(level.Adapt<LevelResponse>());
    }
}
