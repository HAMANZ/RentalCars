using FleetErp.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetErp.Infrastructure.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transactions");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Amount)
            .HasPrecision(18, 2);

        builder.Property(t => t.ReferenceType)
            .HasMaxLength(50);

        builder.Property(t => t.Notes)
            .HasMaxLength(500);

        // Indexes
        builder.HasIndex(t => t.TransactionTypeId);
        builder.HasIndex(t => t.DebitAccountId);
        builder.HasIndex(t => t.CreditAccountId);
        builder.HasIndex(t => new { t.ReferenceType, t.ReferenceId });
        builder.HasIndex(t => t.OccurredAt);
        builder.HasIndex(t => t.IsDeleted);

        // Relationships
        builder.HasOne(t => t.TransactionType)
            .WithMany()
            .HasForeignKey(t => t.TransactionTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.DebitAccount)
            .WithMany(a => a.DebitTransactions)
            .HasForeignKey(t => t.DebitAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.CreditAccount)
            .WithMany(a => a.CreditTransactions)
            .HasForeignKey(t => t.CreditAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        // Global query filter for soft delete
        builder.HasQueryFilter(t => !t.IsDeleted);
    }
}
