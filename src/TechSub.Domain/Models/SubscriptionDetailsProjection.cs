using TechSub.Domain.Enuns;

namespace TechSub.Domain.Models;

public record SubscriptionDetailsProjection(
    int PlanId,
    string PlanName,
    decimal MonthlyPrice,
    decimal AnnualPrice,
    int UserId,
    string UserName,
    string UserEmail,
    ESubscriptionStatus Status,
    EBillingCycle Cycle
);