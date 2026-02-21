using Universe.Application.LevelServices.LevelDtos;

namespace Universe.Application.LevelServices.Queries.GetLevel;

public class GetLevelQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetLevelQuery, Result<LevelResponse>>
{
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<Result<LevelResponse>> Handle(GetLevelQuery request, CancellationToken cancellationToken = default)
    {
        var result = await unitOfWork.LevelRepository.GetByIdAsync(request.Id, cancellationToken);
        if (result.IsSuccess)
        {
            var response = result.Value.Adapt<LevelResponse>();
            return Result.Success(response);
        }
        return Result.Failure<LevelResponse>(result.Error);
    }
}
