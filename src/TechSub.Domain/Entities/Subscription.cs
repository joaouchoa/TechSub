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

    public DateTime? NextBillingDate { get; private set; }
    public bool CancelAtPeriodEnd { get; private set; }

    private Subscription() { }

    public Subscription(int userId, int planId, EBillingCycle cycle, bool planHasTrial)
    {
        UserId = userId;
        PlanId = planId;
        Cycle = cycle;
        CancelAtPeriodEnd = false;

        if (planHasTrial)
        {
            Status = ESubscriptionStatus.Trialing;
            TrialEndDate = GetBrazilianTime().AddDays(7);
            NextBillingDate = TrialEndDate;
        }
        else
        {
            Status = ESubscriptionStatus.Active;
            TrialEndDate = null;
            NextBillingDate = GetBrazilianTime();
        }
    }

    public void Cancel()
    {
        if (Status == ESubscriptionStatus.Trialing)
        {
            Status = ESubscriptionStatus.Canceled;
        }
        else if (Status == ESubscriptionStatus.Active)
        {
            CancelAtPeriodEnd = true;
        }

        UpdateTimestamp();
    }

    public void ExecuteScheduledCancellation()
    {
        Status = ESubscriptionStatus.Canceled;
        NextBillingDate = null;    
        CancelAtPeriodEnd = false;

        UpdateTimestamp();
    }

    public void ExtendBillingCycle()
    {
        Status = ESubscriptionStatus.Active;
        CancelAtPeriodEnd = false;
        CalculateNextBillingDate();
        UpdateTimestamp();
    }

    private void CalculateNextBillingDate()
    {
        var baseDate = NextBillingDate ?? GetBrazilianTime();
        NextBillingDate = Cycle == EBillingCycle.Monthly
            ? baseDate.AddMonths(1)
            : baseDate.AddYears(1);
    }

    public void DowngradeToFreePlan()
    {
        PlanId = 1;
        Cycle = EBillingCycle.Monthly;
        NextBillingDate = null;       
        TrialEndDate = null;          
        Status = ESubscriptionStatus.Active; 
        CancelAtPeriodEnd = false;   

        UpdateTimestamp();
    }
}