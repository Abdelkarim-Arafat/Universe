using Universe.Application.ExamCommitteeServices.Dtos;
namespace Universe.Application.ExamCommitteeServices.Queries.Get;

public class GetAllExamCommitteesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllExamCommitteesQuery, Result<PaginationList<ExamCommitteeResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<ExamCommitteeResponse>>> Handle(GetAllExamCommitteesQuery request, CancellationToken cancellationToken)
    {
        var isExamTermExist = await _unitOfWork.ExamRepository
            .IsExistExamTermAsync(request.ExamTermId, cancellationToken);

        if (!isExamTermExist)
            return Result.Failure<PaginationList<ExamCommitteeResponse>>(ExamErrors.ExamTermNotFound);

        var query = _unitOfWork.Repository<ExamCommittee>().GetQueryable()
            .Where(com => com.ExamTermId == request.ExamTermId && !com.IsDeleted);

        var source = query.Select(com => new ExamCommitteeResponse(
                  com.Id,
                  com.MaxCapacity,
                  com.CommitteeNumber,
                  com.Room != null
                  ? $"{com.Room.RoomNumber} - {com.Room.Building.Name}"
                  : "No Place"
                  ));

        var filter = request.Filter;

        if (!string.IsNullOrEmpty(filter.SearchValue))
            source = source.Where(x =>
                x.Place.Contains(filter.SearchValue) ||
                x.CommitteeNumber.ToString().Contains(filter.SearchValue));
        
        if (!string.IsNullOrEmpty(filter.SortColumn))
            source = source.OrderBy($"{filter.SortColumn} {filter.SortDirection}");

        var response = await PaginationList<ExamCommitteeResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}