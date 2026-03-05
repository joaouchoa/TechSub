using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechSub.Application.Users.Commands.CreateUser;
using TechSub.Application.Users.Queries.LoginUser;

namespace TechSub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserQuery query)
    {
        var result = await _mediator.Send(query);
        return result.ToActionResult();
    }
}