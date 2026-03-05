using TechSub.Domain.Entities;

namespace TechSub.Domain.Repositories
{
    public interface IPlanRepository
    {
        Task<int> AddAsync (Plan plan, CancellationToken cancellationToken = default);
        Task<IEnumerable<Plan?>> GetAllActiveAsync(CancellationToken cancellationToken = default);
    }
}
