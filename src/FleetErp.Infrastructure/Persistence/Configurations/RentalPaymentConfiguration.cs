using FleetErp.Domain.Entities.Rentals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetErp.Infrastructure.Persistence.Configurations;

public class RentalPaymentConfiguration : IEntityTypeConfiguration<RentalPayment>
{
    public void Configure(EntityTypeBuilder<RentalPayment> builder)
    {
        builder.ToTable("rental_payments");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Amount)
            .HasPrecision(18, 2);

        builder.Property(p => p.TransactionReference)
            .HasMaxLength(100);

        builder.Property(p => p.Notes)
            .HasMaxLength(500);

        // Indexes
        builder.HasIndex(p => p.RentalId);
        builder.HasIndex(p => p.PaymentMethodId);
        builder.HasIndex(p => p.PaymentDate);
        builder.HasIndex(p => p.IsDeleted);

        // Relationships
        builder.HasOne(p => p.PaymentMethod)
            .WithMany()
            .HasForeignKey(p => p.PaymentMethodId)
            .OnDelete(DeleteBehavior.Restrict);

        // Global query filter for soft delete
        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}
