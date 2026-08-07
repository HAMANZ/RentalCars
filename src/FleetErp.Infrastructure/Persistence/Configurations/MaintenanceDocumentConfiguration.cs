using FleetErp.Domain.Entities.Maintenance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetErp.Infrastructure.Persistence.Configurations;

public class MaintenanceDocumentConfiguration : IEntityTypeConfiguration<MaintenanceDocument>
{
    public void Configure(EntityTypeBuilder<MaintenanceDocument> builder)
    {
        builder.ToTable("maintenance_documents");

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
        builder.HasIndex(d => d.MaintenanceRecordId);
        builder.HasIndex(d => d.DocumentTypeId);
        builder.HasIndex(d => d.IsDeleted);

        // Relationships
        builder.HasOne(d => d.DocumentType)
            .WithMany()
            .HasForeignKey(d => d.DocumentTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Global query filter for soft delete
        builder.HasQueryFilter(d => !d.IsDeleted);
    }
}
