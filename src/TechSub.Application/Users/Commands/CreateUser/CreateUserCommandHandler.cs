using MediatR;
using FluentValidation;
using TechSub.Domain.Utils;
using TechSub.Domain.Entities;
using TechSub.Domain.Repositories;
using TechSub.Application.Interfaces;
using TechSub.Application.Users.Messages;

namespace TechSub.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<int>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IValidator<CreateUserCommand> _validator;

    public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IValidator<CreateUserCommand> validator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _validator = validator;
    }

    public async Task<Result<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
            return Result<int>.BadRequest(errors);
        }

        var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (existingUser != null)
            return Result<int>.Conflict(UserMessages.ERRO001_EmailAlreadyInUse);

        var passwordHash = _passwordHasher.Hash(request.Password);

        var user = new User(request.Name, request.Email, passwordHash);

        var newUserId = await _userRepository.AddAsync(user, cancellationToken);

        return Result<int>.Created(newUserId);
    }
}