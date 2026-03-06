using MediatR;
using TechSub.Application.Users.Messages;
using TechSub.Domain.Entities;
using TechSub.Domain.Enuns;
using TechSub.Domain.Repositories;
using TechSub.Domain.Utils;

namespace TechSub.Application.Payments.Commands.SimulatePayment;

public class SimulatePaymentCommandHandler : IRequestHandler<SimulatePaymentCommand, Result<int>>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IPlanRepository _planRepository;
    private readonly IPaymentRepository _paymentRepository;

    public SimulatePaymentCommandHandler(
        ISubscriptionRepository subscriptionRepository,
        IPlanRepository planRepository,
        IPaymentRepository paymentRepository)
    {
        _subscriptionRepository = subscriptionRepository;
        _planRepository = planRepository;
        _paymentRepository = paymentRepository;
    }

    public async Task<Result<int>> Handle(SimulatePaymentCommand request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(request.SubscriptionId, cancellationToken);
        if (subscription == null)
            return Result<int>.BadRequest(ValidationMessages.ERRO024_SubscriptionNotExists);

        if (subscription.Status == ESubscriptionStatus.Canceled)
            return Result<int>.BadRequest(ValidationMessages.ERRO025_NotPossibleToChargeCancelledSubscription);

        var plan = await _planRepository.GetByIdAsync(subscription.PlanId, cancellationToken);
        if (plan == null)
            return Result<int>.BadRequest(ValidationMessages.ERRO026_ThePlanIsNoLongerExists);

        decimal priceToCharge = subscription.Cycle == EBillingCycle.Monthly
            ? plan.MonthlyPrice
            : plan.AnnualPrice;

        var payment = new Payment(subscription.Id, priceToCharge);

        if (request.SimulateSuccess)
        {
            var fakeTransactionId = $"txn_{Guid.NewGuid().ToString().Substring(0, 8)}";
            payment.MarkAsSuccess(fakeTransactionId);

            subscription.ExtendBillingCycle();
        }
        else
        {
            payment.MarkAsFailed("Cartão recusado por falta de saldo.");
        }

        await _paymentRepository.AddAsync(payment, cancellationToken);
        await _subscriptionRepository.UpdateAsync(subscription, cancellationToken);

        return Result<int>.Sucess(payment.Id);
    }
}