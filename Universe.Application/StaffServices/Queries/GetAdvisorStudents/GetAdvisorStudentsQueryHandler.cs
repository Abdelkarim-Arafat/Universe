namespace Universe.Application.StaffServices.Queries.GetAdvisorStudents;

public class GetAdvisorStudentsQueryHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetAdvisorStudentsQuery, Result<PaginationList<StudentResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<StudentResponse>>> Handle(GetAdvisorStudentsQuery request, CancellationToken cancellationToken)
    {
        var filter = request.Filter;

        var query = _unitOfWork.Repository<Student>()
            .GetQueryable()
            .AsNoTracking()
            .Where(x => x.AdvisorId == request.AdvisorId && !x.IsDeleted);

        if (!string.IsNullOrEmpty(filter.SearchValue))
        {
            query = query.Where(x => x.Name.Contains(filter.SearchValue) || x.StudentCode.Contains(filter.SearchValue));
        }

        if (!string.IsNullOrEmpty(filter.SortColumn))
        {
            query = query.OrderBy($"{filter.SortColumn} asc");
        }

        var source = query
            .Select(x => new StudentResponse(
                x.Id,
                x.Name,
                x.StudentCode,
                x.NationalIdOrPassport,
                x.Gender,
                x.ImageUrl
            ));

        var response = await PaginationList<StudentResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}