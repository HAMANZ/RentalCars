using FleetErp.Domain.Entities.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetErp.Infrastructure.Persistence.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("vehicles");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PlateNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.Make)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Model)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Color)
            .HasMaxLength(30);

        builder.Property(x => x.Vin)
            .HasMaxLength(50);

        builder.Property(x => x.EngineNumber)
            .HasMaxLength(50);

        builder.Property(x => x.ChassisNumber)
            .HasMaxLength(50);

        builder.Property(x => x.DailyRate)
            .HasPrecision(18, 2);

        builder.Property(x => x.PurchasePrice)
            .HasPrecision(18, 2);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        // Indexes
        builder.HasIndex(x => x.PlateNumber).IsUnique();
        builder.HasIndex(x => x.Vin);
        builder.HasIndex(x => x.InvestorId);
        builder.HasIndex(x => x.StatusId);
        builder.HasIndex(x => x.VehicleTypeId);

        // Relationships
        builder.HasOne(x => x.Investor)
            .WithMany()
            .HasForeignKey(x => x.InvestorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Status)
            .WithMany()
            .HasForeignKey(x => x.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.VehicleType)
            .WithMany()
            .HasForeignKey(x => x.VehicleTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.FuelType)
            .WithMany()
            .HasForeignKey(x => x.FuelTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.TransmissionType)
            .WithMany()
            .HasForeignKey(x => x.TransmissionTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Account)
            .WithMany()
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Documents)
            .WithOne(x => x.Vehicle)
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        // Index for AccountId
        builder.HasIndex(x => x.AccountId);
    }
}
