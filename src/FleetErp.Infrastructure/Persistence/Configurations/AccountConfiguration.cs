using FleetErp.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetErp.Infrastructure.Persistence.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("accounts");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.OwnerType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(a => a.Balance)
            .HasPrecision(18, 2);

        builder.Property(a => a.Currency)
            .IsRequired()
            .HasMaxLength(10);

        // Indexes
        builder.HasIndex(a => a.Code).IsUnique();
        builder.HasIndex(a => new { a.OwnerType, a.OwnerId });
        builder.HasIndex(a => a.AccountTypeId);
        builder.HasIndex(a => a.IsActive);
        builder.HasIndex(a => a.IsDeleted);

        // Relationships
        builder.HasOne(a => a.AccountType)
            .WithMany()
            .HasForeignKey(a => a.AccountTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Global query filter for soft delete
        builder.HasQueryFilter(a => !a.IsDeleted);
    }
}
