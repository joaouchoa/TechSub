using MediatR;
using TechSub.Domain.Utils;

namespace TechSub.Application.Users.Commands.CreateUser
{
    public record CreateUserCommand(string Name, string Email, string Password) : IRequest<Result<int>>;
}
