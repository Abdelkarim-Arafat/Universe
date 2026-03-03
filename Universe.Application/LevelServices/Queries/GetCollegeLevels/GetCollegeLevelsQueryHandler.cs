using Universe.Application.LevelServices.LevelDtos;
 
namespace Universe.Application.LevelServices.Queries.GetAcademicProgramLevels;

public class GetCollegeLevelsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCollegeLevelsQuery, Result<PaginationList<LevelResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<LevelResponse>>> Handle(GetCollegeLevelsQuery request, CancellationToken cancellationToken)
    {
        var isProgramExist = await _unitOfWork.AcademicProgramRepository.IsExistAsync(request.AcademicProgramId, cancellationToken);
        if (!isProgramExist)
            return Result.Failure<PaginationList<LevelResponse>>(AcademicProgramErrors.AcademicProgramNotFound);

        var query = _unitOfWork.Repository<Level>().GetQueryable();

        query = query.Where(l => l.AcademicProgramId == request.AcademicProgramId);
        var filter = request.Filter;

        if (!string.IsNullOrEmpty(filter.SearchValue))
        {
            query = query.Where(x => x.Name.Contains(filter.SearchValue));
        }

        if (!string.IsNullOrEmpty(filter.SortColumn))
        {
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");
        }

        var source = query.ProjectToType<LevelResponse>();

        var response = await PaginationList<LevelResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}
