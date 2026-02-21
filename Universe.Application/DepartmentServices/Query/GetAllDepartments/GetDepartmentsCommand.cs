
using Universe.Application.DepartmentServices.DepartmentDtos;

namespace Universe.Application.DepartmentServices.Query.GetDepartments;

public record GetDepartmentsCommand(
    [Required]Guid CollegeId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<DepartmentResponse>>>;