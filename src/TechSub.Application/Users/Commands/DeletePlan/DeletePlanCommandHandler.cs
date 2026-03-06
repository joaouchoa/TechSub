using MediatR;
using TechSub.Application.Users.Messages;
using TechSub.Domain.Repositories;
using TechSub.Domain.Utils;

namespace TechSub.Application.Plans.Commands.DeletePlan;

public class DeletePlanCommandHandler : IRequestHandler<DeletePlanCommand, Result<bool>>
{
    private readonly IPlanRepository _planRepository;

    public DeletePlanCommandHandler(IPlanRepository planRepository)
    {
        _planRepository = planRepository;
    }

    public async Task<Result<bool>> Handle(DeletePlanCommand request, CancellationToken cancellationToken)
    {
        var plan = await _planRepository.GetByIdAsync(request.Id, cancellationToken);

        if (plan == null)
            return Result<bool>.BadRequest(ValidationMessages.ERRO018_PlanNotFound);

        plan.Deactivate();

        await _planRepository.UpdateAsync(plan, cancellationToken);

        return Result<bool>.Sucess(true);
    }
}