using FleetErp.Domain.Entities.Maintenance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetErp.Infrastructure.Persistence.Configurations;

public class MaintenanceRecordConfiguration : IEntityTypeConfiguration<MaintenanceRecord>
{
    public void Configure(EntityTypeBuilder<MaintenanceRecord> builder)
    {
        builder.ToTable("maintenance_records");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Description)
            .HasMaxLength(500);

        builder.Property(m => m.Cost)
            .HasPrecision(18, 2);

        builder.Property(m => m.ServiceProvider)
            .HasMaxLength(200);

        builder.Property(m => m.Notes)
            .HasMaxLength(1000);

        // Indexes
        builder.HasIndex(m => m.VehicleId);
        builder.HasIndex(m => m.MaintenanceTypeId);
        builder.HasIndex(m => m.StatusId);
        builder.HasIndex(m => m.ScheduledDate);
        builder.HasIndex(m => m.CompletedDate);
        builder.HasIndex(m => m.IsDeleted);

        // Relationships
        builder.HasOne(m => m.Vehicle)
            .WithMany()
            .HasForeignKey(m => m.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.MaintenanceType)
            .WithMany()
            .HasForeignKey(m => m.MaintenanceTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Status)
            .WithMany()
            .HasForeignKey(m => m.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(m => m.Documents)
            .WithOne(d => d.MaintenanceRecord)
            .HasForeignKey(d => d.MaintenanceRecordId)
            .OnDelete(DeleteBehavior.Cascade);

        // Global query filter for soft delete
        builder.HasQueryFilter(m => !m.IsDeleted);
    }
}
