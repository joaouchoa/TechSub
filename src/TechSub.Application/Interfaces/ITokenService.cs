using TechSub.Domain.Entities;

namespace TechSub.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}