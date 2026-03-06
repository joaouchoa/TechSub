using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechSub.Application.Plans.Commands.CreatePlan;
using TechSub.Application.Plans.Commands.DeletePlan;
using TechSub.Application.Plans.Commands.UpdatePlan;
using TechSub.Application.Plans.Queries.GetAllActivePlans;

namespace TechSub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PlansController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlansController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreatePlan([FromBody] CreatePlanCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllPlans()
    {
        var result = await _mediator.Send(new GetAllActivePlansQuery());
        return result.ToActionResult();
    }

    [HttpPut()]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdatePlan([FromBody] UpdatePlanCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")] 
    public async Task<IActionResult> DeletePlan(int id)
    {
        var command = new DeletePlanCommand(id);
        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }
}