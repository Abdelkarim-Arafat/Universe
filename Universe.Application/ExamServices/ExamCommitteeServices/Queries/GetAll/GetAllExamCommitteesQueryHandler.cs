using Universe.Application.ExamCommitteeServices.Dtos;
namespace Universe.Application.ExamCommitteeServices.Queries.Get;

public class GetAllExamCommitteesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllExamCommitteesQuery, Result<PaginationList<ExamCommitteeResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<ExamCommitteeResponse>>> Handle(GetAllExamCommitteesQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Repository<ExamCommittee>().GetQueryable()
            .Where(com => com.ExamTermId == request.ExamTermId && !com.IsDeleted);

        var filter = request.Filter;

        //if (!string.IsNullOrEmpty(filter.SearchValue))
        //{
        //    query = query.Where(x => x.CommitteeNumber == filter.SearchValue);
        //}

        if (!string.IsNullOrEmpty(filter.SortColumn))
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");
        

        var source = query.Select(com => com.Adapt<ExamCommitteeResponse>());

        var response = await PaginationList<ExamCommitteeResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}