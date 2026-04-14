using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Querys.GetStudentsWithoutAdvisor;
public class GetStudentsWithoutAdvisorQueryHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<GetStudentsWithoutAdvisorQuery, Result<PaginationList<StudentResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<StudentResponse>>> Handle(
        GetStudentsWithoutAdvisorQuery request,
        CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Repository<Student>()
            .GetQueryable()
            .Where(x => x.CollegeId == request.CollegeId &&
                x.AdvisorId  == null &&
                !x.IsDeleted);

        var filter = request.filter;

        if (!string.IsNullOrEmpty(filter.SearchValue))
        {
            query = query.Where(x => x.Name.Contains(filter.SearchValue));
        }

        if (!string.IsNullOrEmpty(filter.SortColumn))
        {
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");
        }

        var source = query.Select(x => new StudentResponse
        (
            x.Id,
            x.Name,
            x.StudentCode,
            x.NationalIdOrPassport,
            x.Gender
        ));

        var response = await PaginationList<StudentResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}