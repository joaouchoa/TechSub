using FluentValidation;
using TechSub.Application.Users.Messages;

namespace TechSub.Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(UserMessages.ERRO004_NameRequired)
            .MaximumLength(100).WithMessage(UserMessages.ERRO005_NameMaxLength);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(UserMessages.ERRO006_EmailRequired)
            .EmailAddress().WithMessage(UserMessages.ERRO007_EmailInvalidFormat);

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(UserMessages.ERRO008_PasswordRequired)
            .MinimumLength(6).WithMessage(UserMessages.ERRO009_PasswordMinLength)
            .Matches(@"[A-Z]+").WithMessage(UserMessages.ERRO010_PasswordRequiresUppercase)
            .Matches(@"[a-z]+").WithMessage(UserMessages.ERRO011_PasswordRequiresLowercase)
            .Matches(@"[0-9]+").WithMessage(UserMessages.ERRO012_PasswordRequiresNumber);
    }
}