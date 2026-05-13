<<<<<<<< HEAD:Universe.Application/ExamServices/ExamCommitteeServices/Queries/GetExamTermCommittees/GetExamTermCommitteesQueryHandler.cs
========
using Universe.Application.ExamCommitteeServices.Dtos;
namespace Universe.Application.ExamCommitteeServices.Queries.GetAll;
>>>>>>>> 4af299b699488d181e33aa6b8cb24bc5218cbf57:Universe.Application/ExamCommitteeServices/Queries/GetAll/GetAllExamCommitteesQueryHandler.cs

namespace Universe.Application.ExamCommitteeServices.Queries.GetExamTermCommittees;

public class GetExamTermCommitteesQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService)
    : IRequestHandler<GetExamTermCommitteesQuery, Result<PaginationList<ExamCommitteeResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<PaginationList<ExamCommitteeResponse>>> Handle(GetExamTermCommitteesQuery request, CancellationToken cancellationToken)
    {
        var isExamTermExist = await _unitOfWork.ExamRepository
            .IsExistExamTermAsync(request.ExamTermId, cancellationToken);

        if (!isExamTermExist)
            return Result.Failure<PaginationList<ExamCommitteeResponse>>(ExamErrors.ExamTermNotFound);

        var filter = request.Filter;

        var query = _unitOfWork.Repository<ExamCommittee>().GetQueryable()
            .Where(com => com.ExamTermId == request.ExamTermId && !com.IsDeleted);


        if (!string.IsNullOrEmpty(filter.SearchValue))
            query = query.Where(com =>
                com.CommitteeNumber.ToString().Contains(filter.SearchValue) );

        if (!string.IsNullOrEmpty(filter.SortColumn))
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");

        var source = query.Select(com => new ExamCommitteeResponse(
                  com.Id,
                  com.MaxCapacity,
                  com.CommitteeNumber,
                  com.Room != null
                  ? $"{com.Room.RoomNumber} - {com.Room.Building.Name}"
                  : "No Place"
                  ));

        var cacheKey = ExamCommitteeCacheKeys.List(request.ExamTermId, filter.SearchValue, filter.SortColumn, filter.SortDirection, filter.PageNumber, filter.PageSize);
        var tags = ExamCommitteeCacheKeys.Tags(request.ExamTermId);

        var response = await _cacheService.GetOrCreateAsync(
            key: cacheKey,
            factory: async () => await PaginationList<ExamCommitteeResponse>.CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken),
            cancellationToken: cancellationToken,
            tags: tags
        );

        return Result.Success(response);
    }
}