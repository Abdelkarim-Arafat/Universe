namespace Universe.Application.StudentServices.Queries.GetMilitaryData;


public record GetMilitaryDataQuery(
    [Required] Guid StudentId
) : IRequest<Result<MilitaryDataResponse>>;