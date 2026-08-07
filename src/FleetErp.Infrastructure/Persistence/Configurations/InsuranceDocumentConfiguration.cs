using FleetErp.Domain.Entities.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetErp.Infrastructure.Persistence.Configurations;

public class InsuranceDocumentConfiguration : IEntityTypeConfiguration<InsuranceDocument>
{
    public void Configure(EntityTypeBuilder<InsuranceDocument> builder)
    {
        builder.ToTable("insurance_documents");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.FileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(d => d.FilePath)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(d => d.Description)
            .HasMaxLength(500);

        // Indexes
        builder.HasIndex(d => d.InsuranceRecordId);
        builder.HasIndex(d => d.DocumentTypeId);
        builder.HasIndex(d => d.ExpiresAt);
        builder.HasIndex(d => d.IsDeleted);

        // Relationships
        builder.HasOne(d => d.InsuranceRecord)
            .WithMany(i => i.Documents)
            .HasForeignKey(d => d.InsuranceRecordId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(d => d.DocumentType)
            .WithMany()
            .HasForeignKey(d => d.DocumentTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Global query filter for soft delete
        builder.HasQueryFilter(d => !d.IsDeleted);
    }
}
