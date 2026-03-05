using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSub.Domain.Entities;
using TechSub.Domain.Repositories;
using TechSub.Domain.Utils;

namespace TechSub.Application.Plans.Commands.CreatePlan;

public class CreatePlanCommandHandler : IRequestHandler<CreatePlanCommand, Result<int>>
{
    private readonly IPlanRepository _planRepository;
    private readonly IValidator<CreatePlanCommand> _validator;

    public CreatePlanCommandHandler(IPlanRepository planRepository, IValidator<CreatePlanCommand> validator)
    {
        _planRepository = planRepository;
        _validator = validator;
    }

    public async Task<Result<int>> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
            return Result<int>.BadRequest(errors);
        }

        var plan = new Plan(
            request.Name,
            request.MonthlyPrice,
            request.AnnualPrice,
            request.IsTrialEligible,
            request.Category);

        var newPlanId = await _planRepository.AddAsync(plan, cancellationToken);

        return Result<int>.Created(newPlanId);
    }
}

