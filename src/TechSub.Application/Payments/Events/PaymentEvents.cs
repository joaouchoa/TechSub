using MediatR;

namespace TechSub.Application.Payments.Events;

public record PaymentSucceededEvent(
    int SubscriptionId,
    decimal Amount,
    string TransactionId,
    string UserEmail) : INotification;

public record PaymentFailedEvent(
    int SubscriptionId,
    decimal Amount,
    string Reason,
    string UserEmail) : INotification;