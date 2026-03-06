using TechSub.Domain.Entities;
using TechSub.Domain.Repositories;
using TechSub.Infrastructure.Data;

namespace TechSub.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _dbContext;

    public PaymentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> AddAsync(Payment payment, CancellationToken cancellationToken)
    {
        await _dbContext.Payments.AddAsync(payment, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return payment.Id;
    }
}