using Universe.Core.Contracts.ServiceRequest;
namespace Universe.Application.AcademicServiceRequestServices.Queries.GetStudentServiceRequestHistory;

public record GetStudentServiceRequestHistoryQuery(
    FilterRequest Filter
) : IRequest<Result<PaginationList<ServiceRequestHistoryResponse>>>;