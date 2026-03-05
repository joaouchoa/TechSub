using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSub.Application.Users.Messages;
using TechSub.Domain.Enuns;

namespace TechSub.Application.Plans.Commands.CreatePlan;

public class CreatePlanCommandValidator : AbstractValidator<CreatePlanCommand>
{
    public CreatePlanCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ValidationMessages.ERRO101_NameRequired);

        RuleFor(x => x.MonthlyPrice)
            .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.ERRO102_InvalidMonthlyPrice);

        RuleFor(x => x.AnnualPrice)
            .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.ERRO103_InvalidAnnualPrice);

        RuleFor(x => x.Category)
            .IsInEnum().WithMessage(ValidationMessages.ERRO104_InvalidCategory);

        RuleFor(x => x.IsTrialEligible)
            .Equal(false)
            .When(x => x.Category == ECategory.Free)
            .WithMessage(ValidationMessages.ERRO105_FreePlanCannotHaveTrial);
    }
}

