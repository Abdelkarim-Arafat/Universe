using Universe.Core.Contracts.Staff;

namespace Universe.Application.StaffServices.Queries.GetStaff;

public record GetStaffQuery(
    [Required] Guid UserId
) : IRequest<Result<StaffWithDetailsResponse>>;