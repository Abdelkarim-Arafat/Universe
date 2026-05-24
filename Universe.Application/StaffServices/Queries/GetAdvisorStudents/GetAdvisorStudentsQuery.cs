namespace Universe.Application.StaffServices.Queries.GetAdvisorStudents;

public record GetAdvisorStudentsQuery(
    [Required] Guid AdvisorId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<StudentResponse>>>;