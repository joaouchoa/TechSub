using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechSub.Application.Payments.Commands.SimulatePayment;

namespace TechSub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("simulate")]
    public async Task<IActionResult> SimulatePayment([FromBody] SimulatePaymentCommand command)
    {
        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }
}