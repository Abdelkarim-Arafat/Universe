using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Dynamic.Core;
using Universe.Application.DepartmentServices.DepartmentDtos;

namespace Universe.Application.DepartmentServices.Query.GetDepartments;

public class GetAllDepartmentsCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetDepartmentsCommand, Result<PaginationList<DepartmentResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<DepartmentResponse>>> Handle(GetDepartmentsCommand request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Repository<Department>()
            .GetQueryable()
            .Where(d => d.CollegeId == request.CollegeId);

        var filter = request.Filter;

        if (!string.IsNullOrEmpty(filter.SearchValue))
        {
            query = query.Where(x => x.Name.Contains(filter.SearchValue));
        }

        if (!string.IsNullOrEmpty(filter.SortColumn))
        {
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");
        }

        var source = query.ProjectToType<DepartmentResponse>();

        var response = await PaginationList<DepartmentResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}
