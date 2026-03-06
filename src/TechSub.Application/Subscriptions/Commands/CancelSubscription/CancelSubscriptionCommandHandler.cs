using MediatR;
using TechSub.Application.Users.Messages;
using TechSub.Domain.Enuns;
using TechSub.Domain.Repositories;
using TechSub.Domain.Utils;

namespace TechSub.Application.Subscriptions.Commands.CancelSubscription;

public class CancelSubscriptionCommandHandler : IRequestHandler<CancelSubscriptionCommand, Result<bool>>
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public CancelSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<Result<bool>> Handle(CancelSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository.GetByUserIdAsync(request.UserId, cancellationToken);

        if (subscription == null)
            return Result<bool>.BadRequest(ValidationMessages.ERRO020_SubscriptionNotFound);

        if (subscription.Status == ESubscriptionStatus.Canceled)
            return Result<bool>.BadRequest(ValidationMessages.ERRO021_SubscriptionAlreadyCanceled);

        subscription.Cancel();

        await _subscriptionRepository.UpdateAsync(subscription, cancellationToken);

        return Result<bool>.Sucess(true);
    }
}