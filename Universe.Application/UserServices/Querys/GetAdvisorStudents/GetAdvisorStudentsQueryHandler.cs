using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.Querys.GetAllStudents;
using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Querys.GetAdvisorStudents;

public class GetAdvisorStudentsQueryHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetAdvisorStudentsQuery, Result<PaginationList<StudentResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<StudentResponse>>> Handle(GetAdvisorStudentsQuery request, CancellationToken cancellationToken)
    {
        var filter = request.Filter;

        var source = _unitOfWork.Repository<Student>()
            .GetQueryable()
            .Where(x => x.AdvisorId == request.AdvisorId && !x.IsDeleted)
            .ApplySearch(filter.SearchValue, x => x.Name, x => x.StudentCode)
            .Select(x => new StudentResponse(
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