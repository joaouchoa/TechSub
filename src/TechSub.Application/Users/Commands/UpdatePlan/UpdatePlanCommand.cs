using MediatR;
using TechSub.Domain.Enuns;
using TechSub.Domain.Utils;

namespace TechSub.Application.Plans.Commands.UpdatePlan;

public record UpdatePlanCommand(
    int Id,
    string Name,
    decimal MonthlyPrice,
    decimal AnnualPrice,
    bool IsTrialEligible,
    ECategory Category) : IRequest<Result<int>>;