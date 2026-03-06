using TechSub.Domain.Entities;

namespace TechSub.Domain.Repositories
{
    public interface IPlanRepository
    {
        Task<int> AddAsync (Plan plan, CancellationToken cancellationToken = default);
        Task<IEnumerable<Plan?>> GetAllActiveAsync(CancellationToken cancellationToken = default);
        Task<Plan?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateAsync(Plan plan, CancellationToken cancellationToken);
    }
}
