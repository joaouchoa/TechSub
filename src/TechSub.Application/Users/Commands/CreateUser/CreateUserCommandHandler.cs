using MediatR;
using TechSub.Application.Interfaces;
using TechSub.Application.Users.Commands.CreateUser;
using TechSub.Domain.Entities;
using TechSub.Domain.Repositories;
using TechSub.Domain.Utils;

namespace TechSub.Application.Users.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<int>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (existingUser != null)
        {
            return Result<int>.Conflict("Este e-mail já está em uso.");
        }

        var passwordHash = _passwordHasher.Hash(request.Password);

        var user = new User(request.Name, request.Email, passwordHash);

        var newUserId = await _userRepository.AddAsync(user, cancellationToken);

        return Result<int>.Created(newUserId);
    }
}