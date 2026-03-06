using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechSub.Domain.Entities;

namespace TechSub.Infrastructure.Data.Configurations;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("Subscriptions");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.UserId).IsRequired();
        builder.Property(s => s.PlanId).IsRequired();
        builder.Property(s => s.Cycle).IsRequired();
        builder.Property(s => s.Status).IsRequired();
        builder.Property(s => s.TrialEndDate).IsRequired(false);

        builder.Property(s => s.CreatedAt).IsRequired();
        builder.Property(s => s.UpdatedAt).IsRequired(false);
    }
}