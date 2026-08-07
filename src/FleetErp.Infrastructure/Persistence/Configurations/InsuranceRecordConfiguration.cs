using FleetErp.Domain.Entities.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetErp.Infrastructure.Persistence.Configurations;

public class InsuranceRecordConfiguration : IEntityTypeConfiguration<InsuranceRecord>
{
    public void Configure(EntityTypeBuilder<InsuranceRecord> builder)
    {
        builder.ToTable("insurance_records");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.PolicyNumber)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.Premium)
            .HasPrecision(18, 2);

        builder.Property(i => i.CoverageAmount)
            .HasPrecision(18, 2);

        builder.Property(i => i.Deductible)
            .HasPrecision(18, 2);

        builder.Property(i => i.CoverageDetails)
            .HasMaxLength(1000);

        builder.Property(i => i.Notes)
            .HasMaxLength(1000);

        // Indexes
        builder.HasIndex(i => i.VehicleId);
        builder.HasIndex(i => i.InsuranceCompanyId);
        builder.HasIndex(i => i.InsuranceTypeId);
        builder.HasIndex(i => i.StatusId);
        builder.HasIndex(i => i.PolicyNumber);
        builder.HasIndex(i => i.StartDate);
        builder.HasIndex(i => i.EndDate);
        builder.HasIndex(i => i.IsDeleted);

        // Relationships
        builder.HasOne(i => i.Vehicle)
            .WithMany()
            .HasForeignKey(i => i.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.InsuranceCompany)
            .WithMany(c => c.InsuranceRecords)
            .HasForeignKey(i => i.InsuranceCompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.InsuranceType)
            .WithMany()
            .HasForeignKey(i => i.InsuranceTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.Status)
            .WithMany()
            .HasForeignKey(i => i.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(i => i.Documents)
            .WithOne(d => d.InsuranceRecord)
            .HasForeignKey(d => d.InsuranceRecordId)
            .OnDelete(DeleteBehavior.Cascade);

        // Global query filter for soft delete
        builder.HasQueryFilter(i => !i.IsDeleted);
    }
}
