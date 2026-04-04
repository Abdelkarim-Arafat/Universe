using Universe.Application.EnrollmentServices.Dtos;

namespace Universe.Application.EnrollmentServices.Queries.GetEnrollmentPage;

public record GetEnrollmentPageQuery
(
    [Required] Guid StudentId,   
    [Required] Guid SemesterId,
    [Required] Guid LevelId
) : IRequest<Result<EnrollmentPageResponse>>;
