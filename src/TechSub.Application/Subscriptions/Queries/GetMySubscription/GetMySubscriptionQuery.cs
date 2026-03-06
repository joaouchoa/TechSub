using MediatR;
using TechSub.Domain.Utils;

namespace TechSub.Application.Subscriptions.Queries.GetMySubscription;

public record GetMySubscriptionQuery(int UserId) : IRequest<Result<MySubscriptionViewModel>>;