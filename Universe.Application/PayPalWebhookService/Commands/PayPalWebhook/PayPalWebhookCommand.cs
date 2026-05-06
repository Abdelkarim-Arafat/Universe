using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.PaymentService.Commands.PayPalWebhook;

public record PayPalWebhookCommand(
    string Payload
) : IRequest;