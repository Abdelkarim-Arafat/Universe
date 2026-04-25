using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Universe.Application.PaymentService.Commands.CreateOrder;

namespace Universe.Api.Controllers;

[Route("payments")]
[ApiController]
public class PaymentController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    //[HttpPost("orders")]
    //public async Task<IActionResult> CreateOrder(
    //    [FromBody] CreateOrderCommand request,
    //    CancellationToken cancellationToken)
    //{
    //    var result = await _mediator.Send(request, cancellationToken);

    //    return result.IsSuccess
    //        ? Ok(result.Value)
    //        : result.ToProblem();
    //}
}
