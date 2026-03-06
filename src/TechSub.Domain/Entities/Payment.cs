using System;
using TechSub.Domain.Enuns;

namespace TechSub.Domain.Entities;

public class Payment : BaseEntity
{
    public int SubscriptionId { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentStatus Status { get; private set; }
    public DateTime PaymentDate { get; private set; }
    public string? ExternalTransactionId { get; private set; } // Simulando o ID do Stripe/Pagar.me

    private Payment() { }

    public Payment(int subscriptionId, decimal amount)
    {
        SubscriptionId = subscriptionId;
        Amount = amount;
        Status = PaymentStatus.Pending;
        PaymentDate = DateTime.UtcNow;
    }

    public void MarkAsSuccess(string transactionId)
    {
        Status = PaymentStatus.Success;
        ExternalTransactionId = transactionId;
        UpdateTimestamp();
    }

    public void MarkAsFailed(string errorReason)
    {
        Status = PaymentStatus.Failed;
        ExternalTransactionId = errorReason;
        UpdateTimestamp();
    }
}