using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechSub.Domain.Entities;

namespace TechSub.Infrastructure.Data.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.SubscriptionId).IsRequired();

        builder.Property(p => p.Amount)
               .IsRequired()
               .HasColumnType("numeric(18,2)");

        builder.Property(p => p.Status).IsRequired();
        builder.Property(p => p.PaymentDate).IsRequired();

        builder.Property(p => p.ExternalTransactionId)
               .HasMaxLength(255)
               .IsRequired(false);

        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt).IsRequired(false);
    }
}