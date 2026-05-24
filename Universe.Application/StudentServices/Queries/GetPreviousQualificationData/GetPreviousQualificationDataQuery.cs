namespace Universe.Application.StudentServices.Queries.GetPreviousQualificationData;

public record GetPreviousQualificationDataQuery(
    [Required] Guid StudentId
) : IRequest<Result<PreviousQualificationResponse>>;