namespace Universe.Application.ExamServices.ExamTermServices.Commands.TogglePublisher;

public record TogglePublisherCommand
([Required] Guid Id) : IRequest<Result>;