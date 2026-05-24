using Universe.Core.Contracts.ServiceRequest;

namespace Universe.Application.AcademicServiceRequestServices.Queries.GetAllServiceRequests;

public record GetAllServiceRequestsQuery(
    [Required] Guid CollegeId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<ServiceRequestResponse>>>;