using System.Linq.Dynamic.Core;
using Universe.Application.UserServices.Querys.GetStudentsWithoutAdvisor;
using Universe.Core.Contracts.User;

public class GetStudentsWithoutAdvisorQueryHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<GetStudentsWithoutAdvisorQuery, Result<PaginationList<StudentResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<StudentResponse>>> Handle(
        GetStudentsWithoutAdvisorQuery request,
        CancellationToken cancellationToken)
    {
        var filter = request.Filter;

        var query = _unitOfWork.Repository<StudentAcademicProgram>()
            .GetQueryable()
            .AsNoTracking()
            .Where(x =>
                x.AcademicProgramId == request.ProgramId &&
                x.Currently &&
                !x.Student.IsDeleted &&
                x.Student.AdvisorId == null
            )
            .ApplySearch(filter.SearchValue,
                x => x.Student.Name,
                x => x.Student.StudentCode
            );

        if (!string.IsNullOrEmpty(filter.SortColumn))
        {
            query = query.OrderBy($"Student.{filter.SortColumn}");
        }

        var source = query.Select(x => new StudentResponse(
            x.Student.Id,
            x.Student.Name,
            x.Student.StudentCode,
            x.Student.NationalIdOrPassport,
            x.Student.Gender
        ));

        var response = await PaginationList<StudentResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}