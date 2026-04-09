using System;
using System.Collections.Generic;
using Universe.Application.AcademicProgramServices.AcademicProgramDtos;


namespace Universe.Application.AcademicProgramServices.Query.GetAcademicPrograms;

public class GetAllAcademicProgramsCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetAcademicProgramsCommand, Result<PaginationList<AcademicProgramResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<AcademicProgramResponse>>> Handle(GetAcademicProgramsCommand request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Repository<AcademicProgram>()
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

        var source = query.Select(pro => pro.Adapt<AcademicProgramResponse>());

        var response = await PaginationList<AcademicProgramResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}
