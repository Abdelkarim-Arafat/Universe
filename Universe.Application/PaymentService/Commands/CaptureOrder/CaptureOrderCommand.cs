using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.PaymentService.Commands.CaptureOrder;

public record CaptureOrderCommand(
    string OrderId
) : IRequest<Result>;
