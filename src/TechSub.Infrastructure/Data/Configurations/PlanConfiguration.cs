using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechSub.Domain.Entities;

namespace TechSub.Infrastructure.Data.Configurations;

public class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.ToTable("Plans");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.MonthlyPrice)
            .HasColumnType("numeric(10,2)")
            .IsRequired();

        builder.Property(p => p.AnnualPrice)
            .HasColumnType("numeric(10,2)")
            .IsRequired();

        builder.Property(p => p.IsTrialEligible)
            .IsRequired(); 

        builder.Property(p => p.IsActive)
            .IsRequired(); 

        builder.Property(p => p.Category)
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .IsRequired(false);
    }
}