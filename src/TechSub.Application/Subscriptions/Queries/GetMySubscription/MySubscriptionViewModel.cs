namespace TechSub.Application.Subscriptions.Queries.GetMySubscription;

public record MySubscriptionViewModel(
    int SubscriptionId,
    string PlanName,
    string Status,
    string Cycle,
    DateTime? NextBillingDate,
    bool CancelAtPeriodEnd
);