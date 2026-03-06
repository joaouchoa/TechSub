using TechSub.Domain.Entities;

namespace TechSub.Domain.Repositories;

public interface IPaymentRepository
{
    Task<int> AddAsync(Payment payment, CancellationToken cancellationToken);
}