using MediatR;
using FluentValidation;
using TechSub.Domain.Utils;
using TechSub.Domain.Repositories;
using TechSub.Application.Interfaces;
using TechSub.Application.Users.Messages;

namespace TechSub.Application.Users.Queries.LoginUser;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, Result<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IValidator<LoginUserQuery> _validator;

    public LoginUserQueryHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenService tokenService,
        IValidator<LoginUserQuery> validator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _validator = validator;
    }

    public async Task<Result<string>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
            return Result<string>.BadRequest(errors);
        }

        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user == null)
            return Result<string>.Unauthorized(UserMessages.ERRO003_InvalidCredentials);

        var isPasswordValid = _passwordHasher.Verify(request.Password, user.PasswordHash);
        if (!isPasswordValid)
            return Result<string>.Unauthorized(UserMessages.ERRO003_InvalidCredentials);

        var token = _tokenService.GenerateToken(user);

        return Result<string>.Sucess(token);
    }
}