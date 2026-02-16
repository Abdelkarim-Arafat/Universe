
using Universe.Application.DepartmentServices.DepartmentDtos;

namespace Universe.Application.DepartmentServices.Query.GetDepartments;

public record GetDepartmentsCommand(
    [Required]Guid CollegeId
) : IRequest<Result<List<DepartmentResponse>>>;