using TechSub.Domain.Entities;
using TechSub.Domain.Repositories;
using TechSub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TechSub.Infrastructure.Repositories;

public class PlanRepository : IPlanRepository
{
    private readonly AppDbContext _dbContext;

    public PlanRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> AddAsync(Plan plan, CancellationToken cancellationToken)
    {
        await _dbContext.Plans.AddAsync(plan, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return plan.Id;
    }

    public async Task<Plan?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Plans
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Plan>> GetAllActiveAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Plans
            .AsNoTracking() 
            .Where(p => p.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Plan plan, CancellationToken cancellationToken)
    {
        _dbContext.Plans.Update(plan);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

