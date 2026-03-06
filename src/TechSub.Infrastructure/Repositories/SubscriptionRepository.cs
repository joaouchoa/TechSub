using Microsoft.EntityFrameworkCore;
using TechSub.Domain.Entities;
using TechSub.Domain.Enuns; // Ajuste para o namespace exato do seu SubscriptionStatus
using TechSub.Domain.Repositories;
using TechSub.Infrastructure.Data;

namespace TechSub.Infrastructure.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly AppDbContext _dbContext;

    public SubscriptionRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> AddAsync(Subscription subscription, CancellationToken cancellationToken)
    {
        await _dbContext.Subscriptions.AddAsync(subscription, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return subscription.Id;
    }

    public async Task<Subscription?> GetByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Subscriptions
            .FirstOrDefaultAsync(s => s.UserId == userId, cancellationToken);
    }

    public async Task<bool> UserAlreadyHasActiveSubscriptionAsync(int userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Subscriptions
            .AnyAsync(s => s.UserId == userId &&
                          (s.Status == ESubscriptionStatus.Active || s.Status == ESubscriptionStatus.Trialing),
                          cancellationToken);
    }

    public async Task UpdateAsync(Subscription subscription, CancellationToken cancellationToken)
    {
        _dbContext.Subscriptions.Update(subscription);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> HasUserEverSubscribedAsync(int userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Subscriptions
            .AnyAsync(s => s.UserId == userId, cancellationToken);
    }

    public async Task<Subscription?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Subscriptions
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Subscription>> GetAllActiveAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Subscriptions
            .AsNoTracking()
            .Where(s => s.Status == ESubscriptionStatus.Active || s.Status == ESubscriptionStatus.Trialing)
            .ToListAsync(cancellationToken);
    }
}