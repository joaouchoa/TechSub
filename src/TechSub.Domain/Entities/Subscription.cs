using System;
using TechSub.Domain.Enuns;

namespace TechSub.Domain.Entities;

public class Subscription : BaseEntity
{
    public int UserId { get; private set; }
    public int PlanId { get; private set; }
    public EBillingCycle Cycle { get; private set; }
    public ESubscriptionStatus Status { get; private set; }
    public DateTime? TrialEndDate { get; private set; }

    private Subscription() { }

    public Subscription(int userId, int planId, EBillingCycle cycle, bool planHasTrial)
    {
        UserId = userId;
        PlanId = planId;
        Cycle = cycle;

        if (planHasTrial)
        {
            Status = ESubscriptionStatus.Trialing;
            TrialEndDate = DateTime.UtcNow.AddDays(7);
        }
        else
        {
            Status = ESubscriptionStatus.Active;
            TrialEndDate = null;
        }
    }

    public void Cancel()
    {
        Status = ESubscriptionStatus.Canceled;
        UpdateTimestamp();
    }
}