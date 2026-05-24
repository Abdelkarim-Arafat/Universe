using Universe.Core.Contracts.Staff;

namespace Universe.Application.StaffServices.Queries.GetAllStaff;

public record GetAllStaffQuery(
    [Required] Guid CollegeId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<StaffResponse>>>;
