
using Universe.Core.Enums;

namespace Universe.Application.EnrollmentServices.Queries.GetEnrollmentPage;

public record GetEnrollmentPageQuery
(
    [Required] Guid StudentId,   
    [Required] Guid SemesterId,
    [Required] Guid LevelId
) : IRequest<Result<EnrollmentPageResponse>>;
