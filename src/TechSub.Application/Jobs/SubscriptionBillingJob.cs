using MediatR;
using TechSub.Application.Payments.Commands.SimulatePayment;
using TechSub.Domain.Enuns;
using TechSub.Domain.Repositories;

namespace TechSub.Application.Jobs;

public class SubscriptionBillingJob
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IMediator _mediator;

    public SubscriptionBillingJob(ISubscriptionRepository subscriptionRepository, IMediator mediator)
    {
        _subscriptionRepository = subscriptionRepository;
        _mediator = mediator;
    }

    public async Task ProcessDailyBillingAsync()
    {
        var today = DateTime.UtcNow.Date;
        var dueSubscriptions = await _subscriptionRepository.GetSubscriptionsDueTodayAsync(today, CancellationToken.None);

        foreach (var sub in dueSubscriptions)
        {
            // REGRA 1: Cancelamento Agendado
            if (sub.CancelAtPeriodEnd && sub.NextBillingDate?.Date <= today)
            {
                sub.ExecuteScheduledCancellation();
                await _subscriptionRepository.UpdateAsync(sub, CancellationToken.None);
                continue; // Pula para o próximo cliente
            }

            // REGRA 2 e 3: Fim do Trial ou Renovação Ativa (Tenta cobrar!)
            if (
                (sub.Status == ESubscriptionStatus.Trialing && sub.TrialEndDate?.Date <= today) ||
                (sub.Status == ESubscriptionStatus.Active && sub.NextBillingDate?.Date <= today)
            )
            {
                var paymentResult = await _mediator.Send(new SimulatePaymentCommand(sub.Id, SimulateSuccess: true));

                // Se o pagamento FALHAR
                if (paymentResult.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    // chamoo método oficial do Domínio para fazer o rebaixamento de plano!
                    sub.DowngradeToFreePlan();

                    await _subscriptionRepository.UpdateAsync(sub, CancellationToken.None);
                }
            }
        }
    }
}