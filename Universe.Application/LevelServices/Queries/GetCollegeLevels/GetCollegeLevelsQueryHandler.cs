using Universe.Application.LevelServices.LevelDtos;
 
namespace Universe.Application.LevelServices.Queries.GetCollegeLevels;

public class GetCollegeLevelsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCollegeLevelsQuery, Result<List<LevelResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<LevelResponse>>> Handle(GetCollegeLevelsQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.LevelRepository.GetCollegeLevelsAsync(request.CollegeId);
        if (result.IsSuccess)
        {
            var levels = result.Value.Adapt<List<LevelResponse>>();
            return Result.Success(levels);
        }
        return Result.Failure<List<LevelResponse>>(result.Error);
    }
}
