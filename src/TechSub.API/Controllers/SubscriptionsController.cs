using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechSub.Application.Subscriptions.Commands.CancelSubscription;
using TechSub.Application.Subscriptions.Commands.CreateSubscription;

namespace TechSub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SubscriptionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubscriptionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionCommand command)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");

        if (!int.TryParse(userIdString, out int userId))
            return Unauthorized("Usuário não identificado no token.");

        var finalCommand = command with { UserId = userId };
        var result = await _mediator.Send(finalCommand);

        return result.ToActionResult();
    }

    [HttpDelete]
    public async Task<IActionResult> CancelSubscription()
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");

        if (!int.TryParse(userIdString, out int userId))
            return Unauthorized("Usuário não identificado no token.");

        var command = new CancelSubscriptionCommand(userId);
        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }
}