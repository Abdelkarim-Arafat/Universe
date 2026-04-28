using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Universe.Application.AcademicServiceRequestServices.ServiceRequestDtos;
using Universe.Core.Enums;

namespace Universe.Application.AcademicServiceRequestServices.Queries.GetStudentServiceRequestHistory;
public class GetStudentServiceRequestHistoryQueryHandler(
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContext
    ) : IRequestHandler<GetStudentServiceRequestHistoryQuery, Result<PaginationList<ServiceRequestHistoryResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHttpContextAccessor _httpContext = httpContext;

    public async Task<Result<PaginationList<ServiceRequestHistoryResponse>>> Handle(GetStudentServiceRequestHistoryQuery request, CancellationToken cancellationToken)
    {
        var user = _httpContext.HttpContext!.User;
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var query = _unitOfWork.Repository<ServiceRequest>()
            .GetQueryable()
            .AsNoTracking()
            .Where(x => x.StudentId == Guid.Parse(userId))
            .OrderByDescending(x => x.CreatedAt);

        var filter = request.FilterRequest;

        if (!string.IsNullOrEmpty(filter.SortColumn))
        {
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");
        }

        var source = query.Select(x => new ServiceRequestHistoryResponse(
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
