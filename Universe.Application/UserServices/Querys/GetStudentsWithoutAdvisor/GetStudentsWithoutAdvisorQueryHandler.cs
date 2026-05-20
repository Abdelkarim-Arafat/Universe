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

		var source = _unitOfWork.Repository<StudentAcademicProgram>()
			.GetQueryable()
			.Where(x =>
				x.AcademicProgramId == request.ProgramId &&
				x.Currently && 
				!x.Student.IsDeleted &&
				x.Student.AdvisorId == null
			)
			.ApplySearch(filter.SearchValue,
				x => x.Student.Name,
				x => x.Student.StudentCode
			)
            .OrderBy($"Student.{filter.SortColumn} asc")
            .Select(x => new StudentResponse (
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