using Universe.Core.Contracts.Staff;

namespace Universe.Application.StaffServices.Queries.GetAllStaff;

public class GetAllStaffQueryHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetAllStaffQuery, Result<PaginationList<StaffResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<StaffResponse>>> Handle(GetAllStaffQuery request, CancellationToken cancellationToken)
    {
        var filter = request.Filter;

        var query = _unitOfWork.UserRepository
            .GetAllStaffAsync();

        if (!string.IsNullOrEmpty(filter.SearchValue))
        {
            query = query.Where(x => x.Name.Contains(filter.SearchValue));
        }

        var source = query.Select(x => new StaffResponse(
            x.Id.ToString(),
            x.Name
        ));

        var response = await PaginationList<StaffResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}