namespace Universe.Application.StudentServices.Queries.GetContactData;

public record GetContactDataQuery(
    [Required] Guid StudentId
) : IRequest<Result<ContactDataResponse>>;