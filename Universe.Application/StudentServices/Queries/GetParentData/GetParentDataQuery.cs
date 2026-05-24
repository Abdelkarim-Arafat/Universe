namespace Universe.Application.StudentServices.Queries.GetParentData;

public record GetParentDataQuery(
    [Required] Guid StudentId
) : IRequest<Result<ParentDataResponse>>;