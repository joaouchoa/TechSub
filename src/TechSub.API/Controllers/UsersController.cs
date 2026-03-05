using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TechSub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // 🔒 A MÁGICA ESTÁ AQUI! Ninguém entra sem token.
public class UsersController : ControllerBase
{
    [HttpGet("me")]
    public IActionResult GetMe()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var name = User.FindFirst("name")?.Value;

        return Ok(new
        {
            Message = "Você está 100% autenticado!",
            UserId = userId,
            Email = email,
            Name = name
        });
    }
}