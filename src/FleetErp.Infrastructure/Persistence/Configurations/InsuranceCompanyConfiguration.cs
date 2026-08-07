using FleetErp.Domain.Entities.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetErp.Infrastructure.Persistence.Configurations;

public class InsuranceCompanyConfiguration : IEntityTypeConfiguration<InsuranceCompany>
{
    public void Configure(EntityTypeBuilder<InsuranceCompany> builder)
    {
        builder.ToTable("insurance_companies");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(c => c.Phone)
            .HasMaxLength(50);

        builder.Property(c => c.Email)
            .HasMaxLength(150);

        builder.Property(c => c.Address)
            .HasMaxLength(500);

        builder.Property(c => c.ContactPerson)
            .HasMaxLength(150);

        // Indexes
        builder.HasIndex(c => c.Name);
        builder.HasIndex(c => c.IsActive);
        builder.HasIndex(c => c.IsDeleted);

        // Relationships
        builder.HasMany(c => c.InsuranceRecords)
            .WithOne(r => r.InsuranceCompany)
            .HasForeignKey(r => r.InsuranceCompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        // Global query filter for soft delete
        builder.HasQueryFilter(c => !c.IsDeleted);
    }
}
