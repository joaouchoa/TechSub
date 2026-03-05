using FluentValidation;
using TechSub.Application.Users.Messages;

namespace TechSub.Application.Users.Queries.LoginUser;

public class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
{
    public LoginUserQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ValidationMessages.ERRO006_EmailRequired)
            .EmailAddress().WithMessage(ValidationMessages.ERRO007_EmailInvalidFormat);

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(ValidationMessages.ERRO008_PasswordRequired);
    }
}