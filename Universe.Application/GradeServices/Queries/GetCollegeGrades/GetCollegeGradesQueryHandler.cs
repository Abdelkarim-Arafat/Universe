namespace Universe.Application.GradeServices.Queries.GetCollegeGrades;

public class GetCollegeGradesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCollegeGradesQuery, Result<PaginationList<GradeResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<GradeResponse>>> Handle(GetCollegeGradesQuery request, CancellationToken cancellationToken = default)
    {
        var isCollegeExist = await _unitOfWork.CollegeRepository.CheckCollegeIsExistAsync(request.CollegeId, cancellationToken);
        if (!isCollegeExist)
            return Result.Failure<PaginationList<GradeResponse>>(CollegeErrors.NotFound);


        var query = _unitOfWork.Repository<Grade>().GetQueryable();

        query = query.Where(x => x.AcademicProgramId == request.CollegeId);
        var filter = request.Filter;

        if (!string.IsNullOrEmpty(filter.SearchValue))
        {
            query = query.Where(x => x.Name.Contains(filter.SearchValue));
        }

        if (!string.IsNullOrEmpty(filter.SortColumn))
        {
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");
        }

        var source = query.ProjectToType<GradeResponse>();

        var response = await PaginationList<GradeResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}
