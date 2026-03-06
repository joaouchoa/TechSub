using MediatR;
using TechSub.Application.Users.Messages;
using TechSub.Domain.Entities;
using TechSub.Domain.Repositories;
using TechSub.Domain.Utils;

namespace TechSub.Application.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, Result<int>>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IPlanRepository _planRepository;

    public CreateSubscriptionCommandHandler(
        ISubscriptionRepository subscriptionRepository,
        IPlanRepository planRepository)
    {
        _subscriptionRepository = subscriptionRepository;
        _planRepository = planRepository;
    }

    public async Task<Result<int>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var plan = await _planRepository.GetByIdAsync(request.PlanId, cancellationToken);
        if (plan == null || !plan.IsActive)
        {
            return Result<int>.BadRequest(ValidationMessages.ERRO018_PlanNotFound);
        }

        var hasActiveSubscription = await _subscriptionRepository.UserAlreadyHasActiveSubscriptionAsync(request.UserId, cancellationToken);
        if (hasActiveSubscription)
            return Result<int>.BadRequest(ValidationMessages.ERRO019_UserAlreadySubscribed);

        bool shouldApplyTrial = false;

        if (request.OptForTrial)
        {
            if (!plan.IsTrialEligible)
                return Result<int>.BadRequest(ValidationMessages.ERRO023_PlanNotEligibleForTrial);

            bool hasEverSubscribed = await _subscriptionRepository.HasUserEverSubscribedAsync(request.UserId, cancellationToken);
            if (hasEverSubscribed)
                return Result<int>.BadRequest(ValidationMessages.ERRO022_TrialOnlyForNewSubscribers);

            shouldApplyTrial = true;
        }

        var subscription = new Subscription(request.UserId, request.PlanId, request.Cycle, shouldApplyTrial);
        var subscriptionId = await _subscriptionRepository.AddAsync(subscription, cancellationToken);

        return Result<int>.Sucess(subscriptionId);
    }
}