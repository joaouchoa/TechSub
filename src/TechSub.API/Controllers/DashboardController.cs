using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechSub.Application.Subscriptions.Queries.GetDashboardReport;

namespace TechSub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class DashboardController : ControllerBase
{
    private readonly IMediator _mediator;

    public DashboardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("metrics")]
    public async Task<IActionResult> GetMetrics()
    {
        var query = new GetDashboardReportQuery();
        var result = await _mediator.Send(query);

        return result.ToActionResult();
    }
}