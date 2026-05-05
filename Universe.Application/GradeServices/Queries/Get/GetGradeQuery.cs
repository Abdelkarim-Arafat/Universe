namespace Universe.Application.GradeServices.Queries.Get;

public record GetGradeQuery
(
    Guid Id
) : IRequest<Result<GradeResponse>>;
