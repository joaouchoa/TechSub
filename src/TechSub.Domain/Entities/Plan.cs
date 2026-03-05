namespace TechSub.Domain.Entities;

public class Plan : BaseEntity
{
    public string Name { get; private set; }
    public decimal MonthlyPrice { get; private set; }
    public decimal AnnualPrice { get; private set; }
    public bool IsTrialEligible { get; private set; }
    public bool IsActive { get; private set; }

    public Plan(string name, decimal monthlyPrice, decimal annualPrice, bool isTrialEligible)
    {
        Name = name;
        MonthlyPrice = monthlyPrice;
        AnnualPrice = annualPrice;
        IsTrialEligible = isTrialEligible;
        IsActive = true;
    }

    public void UpdateInfo(string name, decimal monthlyPrice, decimal annualPrice, bool isTrialEligible)
    {
        Name = name;
        MonthlyPrice = monthlyPrice;
        AnnualPrice = annualPrice;
        IsTrialEligible = isTrialEligible;
        UpdateTimestamp();
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdateTimestamp();
    }
}