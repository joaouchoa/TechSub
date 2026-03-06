using Microsoft.EntityFrameworkCore;
using TechSub.Domain.Entities;
using TechSub.Domain.Enuns; 
using TechSub.Domain.Models;
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

    public async Task<IEnumerable<SubscriptionDetailsProjection>> GetDashboardRawDataAsync(CancellationToken cancellationToken)
    {
        // Usamos a sintaxe de "Query" do LINQ (parecida com SQL) porque ela é excelente para fazer JOINs
        var query = from s in _dbContext.Subscriptions.AsNoTracking()

                    // 1. O Filtro: Só queremos quem está pagando ou testando
                    where s.Status == ESubscriptionStatus.Active || s.Status == ESubscriptionStatus.Trialing

                    // 2. Os Cruzamentos (JOINs)
                    join p in _dbContext.Plans on s.PlanId equals p.Id
                    join u in _dbContext.Users on s.UserId equals u.Id

                    // 3. A Projeção (O "Select")
                    select new SubscriptionDetailsProjection(
                        p.Id,
                        p.Name,
                        p.MonthlyPrice,
                        p.AnnualPrice,
                        u.Id,
                        u.Name,
                        u.Email,
                        s.Status,
                        s.Cycle
                    );

        // 4. A Execução: Vai no banco e traz a lista pronta
        return await query.ToListAsync(cancellationToken);
    }
}