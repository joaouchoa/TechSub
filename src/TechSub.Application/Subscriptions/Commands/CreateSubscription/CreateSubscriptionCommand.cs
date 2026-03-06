using MediatR;
using System.Text.Json.Serialization;
using TechSub.Domain.Enuns;
using TechSub.Domain.Utils;

namespace TechSub.Application.Subscriptions.Commands.CreateSubscription;

public record CreateSubscriptionCommand(
    [property: JsonIgnore] int UserId,
    int PlanId,
    EBillingCycle Cycle,
    bool OptForTrial
) : IRequest<Result<int>>;