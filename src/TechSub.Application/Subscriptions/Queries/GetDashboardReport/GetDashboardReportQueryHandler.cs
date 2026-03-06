using MediatR;
using TechSub.Domain.Enuns;
using TechSub.Domain.Repositories;
using TechSub.Domain.Utils;

namespace TechSub.Application.Subscriptions.Queries.GetDashboardReport;

public class GetDashboardReportQueryHandler : IRequestHandler<GetDashboardReportQuery, Result<DashboardReportDto>>
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public GetDashboardReportQueryHandler(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<Result<DashboardReportDto>> Handle(GetDashboardReportQuery request, CancellationToken cancellationToken)
    {
        var rawData = await _subscriptionRepository.GetDashboardRawDataAsync(cancellationToken);

        var planReports = new List<PlanReportDto>();
        decimal totalCompanyMRR = 0;

        var groupedByPlan = rawData.GroupBy(r => r.PlanId);

        foreach (var group in groupedByPlan)
        {
            decimal currentPlanMRR = 0;
            var subscribersList = new List<SubscriberDto>();
            string planName = group.First().PlanName;

            foreach (var row in group)
            {
                subscribersList.Add(new SubscriberDto(
                    row.UserId,
                    row.UserName,
                    row.UserEmail,
                    row.Status.ToString(),
                    row.Cycle.ToString()
                ));

                if (row.Status == ESubscriptionStatus.Active)
                {
                    if (row.Cycle == EBillingCycle.Monthly)
                        currentPlanMRR += row.MonthlyPrice;
                    else if (row.Cycle == EBillingCycle.Annual)
                        currentPlanMRR += (row.AnnualPrice / 12);
                }
            }

            planReports.Add(new PlanReportDto(
                planName,
                currentPlanMRR,
                subscribersList.Count,
                subscribersList
            ));

            totalCompanyMRR += currentPlanMRR;
        }

        var dashboard = new DashboardReportDto(totalCompanyMRR, planReports);

        return Result<DashboardReportDto>.Sucess(dashboard);
    }
}