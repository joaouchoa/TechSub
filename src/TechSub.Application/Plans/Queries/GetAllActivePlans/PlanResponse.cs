namespace TechSub.Application.Plans.Queries.GetAllActivePlans;

public record PlanResponse(
    int Id,
    string Name,
    decimal MonthlyPrice,
    decimal AnnualPrice,
    bool IsTrialEligible,
    string Category);

