using FleetErp.Domain.Entities.Rentals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetErp.Infrastructure.Persistence.Configurations;

public class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.ToTable("rentals");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.RentalNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(r => r.DailyRate)
            .HasPrecision(18, 2);

        builder.Property(r => r.Discount)
            .HasPrecision(18, 2);

        builder.Property(r => r.TotalAmount)
            .HasPrecision(18, 2);

        builder.Property(r => r.PaidAmount)
            .HasPrecision(18, 2);

        builder.Property(r => r.Notes)
            .HasMaxLength(1000);

        // Indexes
        builder.HasIndex(r => r.RentalNumber).IsUnique();
        builder.HasIndex(r => r.VehicleId);
        builder.HasIndex(r => r.CustomerId);
        builder.HasIndex(r => r.StatusId);
        builder.HasIndex(r => r.PaymentStatusId);
        builder.HasIndex(r => r.StartDate);
        builder.HasIndex(r => r.EndDate);
        builder.HasIndex(r => r.IsDeleted);

        // Relationships
        builder.HasOne(r => r.Vehicle)
            .WithMany()
            .HasForeignKey(r => r.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Customer)
            .WithMany()
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Status)
            .WithMany()
            .HasForeignKey(r => r.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.PaymentStatus)
            .WithMany()
            .HasForeignKey(r => r.PaymentStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(r => r.Payments)
            .WithOne(p => p.Rental)
            .HasForeignKey(p => p.RentalId)
            .OnDelete(DeleteBehavior.Cascade);

        // Ignore computed properties
        builder.Ignore(r => r.TotalDays);
        builder.Ignore(r => r.SubTotal);
        builder.Ignore(r => r.RemainingAmount);

        // Global query filter for soft delete
        builder.HasQueryFilter(r => !r.IsDeleted);
    }
}
