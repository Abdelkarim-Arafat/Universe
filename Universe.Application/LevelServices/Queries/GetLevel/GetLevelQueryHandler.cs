using Universe.Application.LevelServices.Dtos;

namespace Universe.Application.LevelServices.Queries.GetLevel;

public class GetLevelQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetLevelQuery, Result<LevelResponse>>
{
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<Result<LevelResponse>> Handle(GetLevelQuery request, CancellationToken cancellationToken = default)
    {
        var level = await unitOfWork.LevelRepository.GetByIdAsync(request.Id, cancellationToken);
        if (level is null)
            return Result.Failure<LevelResponse>(LevelErrors.NotFound);

        return Result.Success(level.Adapt<LevelResponse>());
    }
}
