using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicServiceRequestServices.ServiceRequestDtos;
using Universe.Core.Enums;

namespace Universe.Application.AcademicServiceRequestServices.Queries.GetServiceRequestHistory;

public class GetServiceRequestHistoryQueryHandler (
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetServiceRequestHistoryQuery, Result<PaginationList<ServiceRequestHistoryResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<ServiceRequestHistoryResponse>>> Handle(GetServiceRequestHistoryQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Repository<ServiceRequest>()
              .GetQueryable()
              .AsNoTracking()
              .Where(x => x.Service.CollegeId == request.CollegeId && x.Status != RequestStatus.Pending)
              .OrderByDescending(x => x.CreatedAt);

        var filter = request.FilterRequest;

        if (!string.IsNullOrEmpty(filter.SortColumn))
        {
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");
        }

        var source = query.Select(x => new ServiceRequestHistoryResponse (
                x.Payment.Price,
                x.Service.Name,
                x.Student.Name,
                x.Student.StudentCode,
                x.CreatedAt,
                x.UpdatedAt,
                x.Status
        ));

        var response = await PaginationList<ServiceRequestHistoryResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}
