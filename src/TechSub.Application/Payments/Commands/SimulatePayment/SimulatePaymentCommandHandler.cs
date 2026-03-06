using MediatR;
using TechSub.Application.Payments.Events;
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
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;

    public SimulatePaymentCommandHandler(
        ISubscriptionRepository subscriptionRepository,
        IPlanRepository planRepository,
        IPaymentRepository paymentRepository,
        IUserRepository userRepository,
        IMediator mediator)
    {
        _subscriptionRepository = subscriptionRepository;
        _planRepository = planRepository;
        _paymentRepository = paymentRepository;
        _userRepository = userRepository;
        _mediator = mediator;
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

        var user = await _userRepository.GetByIdAsync(subscription.UserId, cancellationToken);
        if (user == null)
            return Result<int>.BadRequest("Usuário atrelado a esta assinatura não foi encontrado.");

        string userEmail = user.Email;

        decimal priceToCharge = subscription.Cycle == EBillingCycle.Monthly
            ? plan.MonthlyPrice
            : plan.AnnualPrice;

        var payment = new Payment(subscription.Id, priceToCharge);
        string fakeTransactionId = "";
        string failReason = "";

        if (request.SimulateSuccess)
        {
            fakeTransactionId = $"txn_{Guid.NewGuid().ToString().Substring(0, 8)}";
            payment.MarkAsSuccess(fakeTransactionId);

            subscription.ExtendBillingCycle();
        }
        else
        {
            failReason = "Cartão recusado por falta de saldo.";
            payment.MarkAsFailed(failReason);
        }

        await _paymentRepository.AddAsync(payment, cancellationToken);
        await _subscriptionRepository.UpdateAsync(subscription, cancellationToken);

        if (request.SimulateSuccess)
        {
            await _mediator.Publish(new PaymentSucceededEvent(subscription.Id, priceToCharge, fakeTransactionId, userEmail), cancellationToken);
        }
        else
        {
            await _mediator.Publish(new PaymentFailedEvent(subscription.Id, priceToCharge, failReason, userEmail), cancellationToken);
            return Result<int>.BadRequest("Falha no processamento do pagamento.");
        }

        return Result<int>.Sucess(payment.Id);
    }
}