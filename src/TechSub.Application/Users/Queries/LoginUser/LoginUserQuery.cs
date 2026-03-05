using MediatR;
using TechSub.Domain.Utils;

namespace TechSub.Application.Users.Queries.LoginUser;

public record LoginUserQuery(string Email, string Password) : IRequest<Result<string>>;