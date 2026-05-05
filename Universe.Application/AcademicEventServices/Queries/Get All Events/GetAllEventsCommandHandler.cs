
using Universe.Application.AcademicEventServices.EvenetDtos;

namespace Universe.Application.AcademicEventServices.Queries.Get_All_Events;

public class GetAllEventsCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllEventsCommand, Result<PaginationList<EventResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<EventResponse>>> Handle(GetAllEventsCommand request, CancellationToken cancellationToken)
    {

        var query = _unitOfWork.Repository<AcademicEvent>()
            .GetQueryable()
            .Where(x => x.ProgramId == request.ProgramId &&
                x.SemesterId == request.SemesterId)
            .OrderBy(x => x.StartDate);

        var filter = request.FilterRequest;

        if (!string.IsNullOrEmpty(filter.SortColumn))
        {
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");
        }

        var source = query.Select(x => new EventResponse(
                x.Id.ToString(),
                x.Type,
                x.StartDate,
                x.EndDate
            ));

        var response = await PaginationList<EventResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}
