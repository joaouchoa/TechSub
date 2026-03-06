using TechSub.Domain.Entities;

namespace TechSub.Domain.Repositories;

public interface ISubscriptionRepository
{
    Task<int> AddAsync(Subscription subscription, CancellationToken cancellationToken);
    Task<Subscription?> GetByUserIdAsync(int userId, CancellationToken cancellationToken);
    Task<bool> UserAlreadyHasActiveSubscriptionAsync(int userId, CancellationToken cancellationToken);
    Task UpdateAsync(Subscription subscription, CancellationToken cancellationToken);
    Task<bool> HasUserEverSubscribedAsync(int userId, CancellationToken cancellationToken);
    Task<Subscription?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<Subscription>> GetAllActiveAsync(CancellationToken cancellationToken);
}