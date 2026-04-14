using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicYearAndSemestersServices.Dtos;
using Universe.Application.AcadimicYearAndSemestersServices.Dtos;
using Universe.Application.UserServices.Querys.GetAllStudents;
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.AcadimicYearAndSemestersServices.Queries.GetAllYears;

public class GetAllYearsCommandHandler(
IUnitOfWork unitOfWork
) : IRequestHandler<GetAllYearsCommand, Result<PaginationList<CurrentAcademicYearResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<CurrentAcademicYearResponse>>> Handle(GetAllYearsCommand request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Repository<AcademicYear>()
            .GetQueryable()
            .Where(x => x.CollegeId == request.CollegeId && !x.IsDeleted);

        var filter = request.Filter;

        if (!string.IsNullOrEmpty(filter.SearchValue))
        {
            query = query.Where(x => x.Name.Contains(filter.SearchValue));
        }

        if (!string.IsNullOrEmpty(filter.SortColumn))
        {
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");
        }

        var source = query.Select(x => new CurrentAcademicYearResponse(x.Id.ToString() , x.Name));

        var response = await PaginationList<CurrentAcademicYearResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}
