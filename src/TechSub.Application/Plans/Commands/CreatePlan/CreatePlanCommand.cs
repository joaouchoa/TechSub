using MediatR;
using TechSub.Domain.Enuns;
using TechSub.Domain.Utils;

namespace TechSub.Application.Plans.Commands.CreatePlan;

public record CreatePlanCommand(
    string Name,
    decimal MonthlyPrice,
    decimal AnnualPrice,
    bool IsTrialEligible,
    ECategory Category) : IRequest<Result<int>>;

