using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.PayPal;

namespace Universe.Application.PaymentService.Commands.CreateOrder;

public record CreateOrderCommand (
    [Required] Guid ServiceId
) : IRequest<Result<CreateOrderResponse>>;
