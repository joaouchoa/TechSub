using MediatR;
using Microsoft.Extensions.Logging;
using TechSub.Application.Payments.Events;

namespace TechSub.Application.Payments.EventHandlers;

public class PaymentSucceededEventHandler : INotificationHandler<PaymentSucceededEvent>
{
    private readonly ILogger<PaymentSucceededEventHandler> _logger;

    public PaymentSucceededEventHandler(ILogger<PaymentSucceededEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(PaymentSucceededEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("\n=============================================");
        _logger.LogInformation($"🚀 E-MAIL ENVIADO: Pagamento de R$ {notification.Amount} aprovado!");
        _logger.LogInformation($"Destinatário: {notification.UserEmail}");
        _logger.LogInformation($"Transação: {notification.TransactionId}");
        _logger.LogInformation("=============================================\n");

        return Task.CompletedTask;
    }
}

public class PaymentFailedEventHandler : INotificationHandler<PaymentFailedEvent>
{
    private readonly ILogger<PaymentFailedEventHandler> _logger;

    public PaymentFailedEventHandler(ILogger<PaymentFailedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(PaymentFailedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogWarning("\n=============================================");
        _logger.LogWarning($"⚠️ E-MAIL ENVIADO: Falha na cobrança de R$ {notification.Amount}!");
        _logger.LogWarning($"Destinatário: {notification.UserEmail}");
        _logger.LogWarning($"Motivo: {notification.Reason}");
        _logger.LogWarning("=============================================\n");

        return Task.CompletedTask;
    }
}