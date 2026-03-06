using MediatR;
using TechSub.Application.Users.Messages;
using TechSub.Domain.Repositories;
using TechSub.Domain.Utils;

namespace TechSub.Application.Plans.Commands.UpdatePlan;

public class UpdatePlanCommandHandler : IRequestHandler<UpdatePlanCommand, Result<int>>
{
    private readonly IPlanRepository _planRepository;

    public UpdatePlanCommandHandler(IPlanRepository planRepository)
    {
        _planRepository = planRepository;
    }

    public async Task<Result<int>> Handle(UpdatePlanCommand request, CancellationToken cancellationToken)
    {
        var plan = await _planRepository.GetByIdAsync(request.Id, cancellationToken);

        if (plan == null)
            return Result<int>.BadRequest(ValidationMessages.ERRO018_PlanNotFound);

        plan.UpdateInfo(
            request.Name,
            request.MonthlyPrice,
            request.AnnualPrice,
            request.IsTrialEligible,
            request.Category);

        await _planRepository.UpdateAsync(plan, cancellationToken);

        return Result<int>.Sucess(plan.Id);
    }
}