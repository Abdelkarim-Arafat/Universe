namespace Universe.Application.PaymentService.Commands.PayPalWebhook;

public record PayPalWebhookCommand(
    string Payload
) : IRequest;