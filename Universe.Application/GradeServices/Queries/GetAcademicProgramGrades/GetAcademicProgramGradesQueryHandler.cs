namespace Universe.Application.GradeServices.Queries.GetAcademicProgramGrades;

public class GetAcademicProgramGradesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAcademicProgramGradesQuery, Result<PaginationList<GradeResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<GradeResponse>>> Handle(GetAcademicProgramGradesQuery request, CancellationToken cancellationToken = default)
    {
        var isAcademicProgramExist = await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(request.AcademicProgramId, cancellationToken);

        if (!isAcademicProgramExist)
            return Result.Failure<PaginationList<GradeResponse>>(AcademicProgramErrors.AcademicProgramNotFound);


        var query = _unitOfWork.Repository<Grade>()
            .GetQueryable()
            .Where(x => x.AcademicProgramId == request.AcademicProgramId);

        var filter = request.Filter;

        if (!string.IsNullOrEmpty(filter.SearchValue))
            query = query.Where(x => x.Name.Contains(filter.SearchValue));
        
        if (!string.IsNullOrEmpty(filter.SortColumn))
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");
 
        var source = query.Select(x => new GradeResponse(
            x.Id,
            x.Name,
            x.Code,
            x.MinScore,
            x.MaxScore,
            x.MinGradePoint,
            x.MaxGradePoint
            ));

        var response = await PaginationList<GradeResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}
