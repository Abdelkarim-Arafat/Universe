using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Universe.Application.PaymentService.Commands.PayPalWebhook;

namespace Universe.Api.Controllers;

[ApiController]
[Route("api/webhooks/paypal")]
public class PayPalWebhookController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("")]
    public async Task<IActionResult> Handle()
    {
        using var reader = new StreamReader(Request.Body);
        var body = await reader.ReadToEndAsync();

        await _mediator.Send(new PayPalWebhookCommand(body));

        return Ok();
    }
}