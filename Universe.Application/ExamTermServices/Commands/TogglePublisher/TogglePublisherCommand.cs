namespace Universe.Application.ExamTermServices.Commands.TogglePublisher;

public record TogglePublisherCommand
([Required] Guid Id) : IRequest<Result>;