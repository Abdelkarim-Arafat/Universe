using Universe.Core.Contracts.PayPal;

namespace Universe.Application.AcademicServiceServices.Commands.CreateOrder;

public record CreateOrderCommand(
    [Required] Guid CollegeId,
    [Required] Guid ServiceId
) : IRequest<Result<CreateOrderResponse>>;
