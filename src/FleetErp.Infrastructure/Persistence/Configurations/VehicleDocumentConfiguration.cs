using FleetErp.Domain.Entities.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetErp.Infrastructure.Persistence.Configurations;

public class VehicleDocumentConfiguration : IEntityTypeConfiguration<VehicleDocument>
{
    public void Configure(EntityTypeBuilder<VehicleDocument> builder)
    {
        builder.ToTable("vehicle_documents");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FilePath)
            .IsRequired()
            .HasMaxLength(500);

        // Ignore computed property
        builder.Ignore(x => x.IsExpired);

        // Indexes
        builder.HasIndex(x => x.VehicleId);
        builder.HasIndex(x => x.DocumentTypeId);

        // Relationships
        builder.HasOne(x => x.DocumentType)
            .WithMany()
            .HasForeignKey(x => x.DocumentTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
