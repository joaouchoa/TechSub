using MediatR;
using TechSub.Domain.Repositories;
using TechSub.Domain.Utils;

namespace TechSub.Application.Subscriptions.Queries.GetMySubscription;

public class GetMySubscriptionQueryHandler : IRequestHandler<GetMySubscriptionQuery, Result<MySubscriptionViewModel>>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IPlanRepository _planRepository;

    public GetMySubscriptionQueryHandler(
        ISubscriptionRepository subscriptionRepository,
        IPlanRepository planRepository)
    {
        _subscriptionRepository = subscriptionRepository;
        _planRepository = planRepository;
    }

    public async Task<Result<MySubscriptionViewModel>> Handle(GetMySubscriptionQuery request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository.GetByUserIdAsync(request.UserId, cancellationToken);

        if (subscription == null)
            return Result<MySubscriptionViewModel>.BadRequest("Você ainda não possui uma assinatura.");

        var plan = await _planRepository.GetByIdAsync(subscription.PlanId, cancellationToken);

        var viewModel = new MySubscriptionViewModel(
            subscription.Id,
            plan?.Name ?? "Plano Desconhecido",
            subscription.Status.ToString(),
            subscription.Cycle.ToString(),  
            subscription.NextBillingDate,
            subscription.CancelAtPeriodEnd
        );

        return Result<MySubscriptionViewModel>.Sucess(viewModel);
    }
}