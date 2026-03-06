namespace TechSub.Application.Subscriptions.Queries.GetDashboardReport;

public record SubscriberDto(
    int UserId,
    string Name,
    string Email,
    string Status,
    string Cycle
);

public record PlanReportDto(
    string PlanName,
    decimal PlanMRR,
    int TotalActiveUsers,
    List<SubscriberDto> Subscribers
);

public record DashboardReportDto(
    decimal TotalMRR,
    List<PlanReportDto> Plans
);