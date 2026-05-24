namespace Universe.Application.StudentServices.Queries.GetPersonalData;

public record GetPersonalDataQuery(
    [Required] Guid StudentId
) : IRequest<Result<PersonalDataResponse>>;