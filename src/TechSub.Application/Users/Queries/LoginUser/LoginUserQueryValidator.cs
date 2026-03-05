using FluentValidation;
using TechSub.Application.Users.Messages;

namespace TechSub.Application.Users.Queries.LoginUser;

public class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
{
    public LoginUserQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(UserMessages.ERRO006_EmailRequired)
            .EmailAddress().WithMessage(UserMessages.ERRO007_EmailInvalidFormat);

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(UserMessages.ERRO008_PasswordRequired);
    }
}