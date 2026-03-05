using FluentValidation;
using TechSub.Application.Users.Messages;

namespace TechSub.Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ValidationMessages.ERRO004_NameRequired)
            .MaximumLength(100).WithMessage(ValidationMessages.ERRO005_NameMaxLength);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ValidationMessages.ERRO006_EmailRequired)
            .EmailAddress().WithMessage(ValidationMessages.ERRO007_EmailInvalidFormat);

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(ValidationMessages.ERRO008_PasswordRequired)
            .MinimumLength(6).WithMessage(ValidationMessages.ERRO009_PasswordMinLength)
            .Matches(@"[A-Z]+").WithMessage(ValidationMessages.ERRO010_PasswordRequiresUppercase)
            .Matches(@"[a-z]+").WithMessage(ValidationMessages.ERRO011_PasswordRequiresLowercase)
            .Matches(@"[0-9]+").WithMessage(ValidationMessages.ERRO012_PasswordRequiresNumber);
    }
}