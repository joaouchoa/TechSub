using System.Text.Json.Serialization;
using MediatR;
using TechSub.Domain.Utils;

namespace TechSub.Application.Subscriptions.Commands.CancelSubscription;

public record CancelSubscriptionCommand(
    [property: JsonIgnore] int UserId
) : IRequest<Result<bool>>;