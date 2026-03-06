using MediatR;
using TechSub.Domain.Enuns;
using TechSub.Domain.Repositories;
using TechSub.Domain.Utils;

namespace TechSub.Application.Subscriptions.Queries.GetDashboardReport;

public class GetDashboardReportQueryHandler : IRequestHandler<GetDashboardReportQuery, Result<DashboardReportDto>>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IPlanRepository _planRepository;
    private readonly IUserRepository _userRepository;

    public GetDashboardReportQueryHandler(
        ISubscriptionRepository subscriptionRepository,
        IPlanRepository planRepository,
        IUserRepository userRepository)
    {
        _subscriptionRepository = subscriptionRepository;
        _planRepository = planRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<DashboardReportDto>> Handle(GetDashboardReportQuery request, CancellationToken cancellationToken)
    {
        var plans = await _planRepository.GetAllActiveAsync(cancellationToken);
        var activeSubscriptions = await _subscriptionRepository.GetAllActiveAsync(cancellationToken);
        var users = await _userRepository.GetAllAsync(cancellationToken);
        var planReports = new List<PlanReportDto>();
        decimal totalCompanyMRR = 0;

        foreach (var plan in plans)
        {
            var subsInThisPlan = activeSubscriptions.Where(s => s.PlanId == plan.Id).ToList();
            decimal currentPlanMRR = 0;
            var subscribersList = new List<SubscriberDto>();

            foreach (var sub in subsInThisPlan)
            {
                var user = users.FirstOrDefault(u => u.Id == sub.UserId);
                if (user != null)
                {
                    subscribersList.Add(new SubscriberDto(
                        user.Id,
                        user.Name,
                        user.Email,
                        sub.Status.ToString(),
                        sub.Cycle.ToString()
                    ));
                }
                if (sub.Status == ESubscriptionStatus.Active)
                {
                    if (sub.Cycle == EBillingCycle.Monthly)
                    {
                        currentPlanMRR += plan.MonthlyPrice;
                    }
                    else if (sub.Cycle == EBillingCycle.Annual)
                    {
                        currentPlanMRR += (plan.AnnualPrice / 12);
                    }
                }
            }
            planReports.Add(new PlanReportDto(
                plan.Name,
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