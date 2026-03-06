using MediatR;
using TechSub.Domain.Utils;

namespace TechSub.Application.Payments.Commands.SimulatePayment;

public record SimulatePaymentCommand(
    int SubscriptionId,
    bool SimulateSuccess = true
) : IRequest<Result<int>>;