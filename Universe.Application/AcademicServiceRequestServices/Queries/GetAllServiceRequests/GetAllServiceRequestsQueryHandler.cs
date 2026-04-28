using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicServiceRequestServices.ServiceRequestDtos;
using Universe.Core.Enums;

namespace Universe.Application.AcademicServiceRequestServices.Queries.GetAllServiceRequests;

internal class GetAllServiceRequestsQueryHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetAllServiceRequestsQuery, Result<PaginationList<ServiceRequestResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<ServiceRequestResponse>>> Handle(GetAllServiceRequestsQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Repository<ServiceRequest>()
            .GetQueryable()
            .AsNoTracking()
            .Where(x => x.Service.CollegeId == request.CollegeId &&
                    x.Status == RequestStatus.Pending)
            .OrderBy(x => x.CreatedAt);

        var filter = request.FilterRequest;

        if (!string.IsNullOrEmpty(filter.SortColumn))
        {
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");
        }

        var source = query.Select(x => new ServiceRequestResponse(
                x.Id.ToString(),
                x.Payment.Price,
                x.Service.Name,
                x.Student.Name,
                x.Student.StudentCode,
                x.CreatedAt
        ));

        var response = await PaginationList<ServiceRequestResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}
