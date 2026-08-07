using FleetErp.Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetErp.Infrastructure.Persistence.Configurations;

public class CustomerDocumentConfiguration : IEntityTypeConfiguration<CustomerDocument>
{
    public void Configure(EntityTypeBuilder<CustomerDocument> builder)
    {
        builder.ToTable("customer_documents");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.FilePath)
            .IsRequired()
            .HasMaxLength(500);

        // Indexes
        builder.HasIndex(d => d.CustomerId);
        builder.HasIndex(d => d.DocumentTypeId);
        builder.HasIndex(d => d.IsDeleted);

        // Relationships
        builder.HasOne(d => d.DocumentType)
            .WithMany()
            .HasForeignKey(d => d.DocumentTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Ignore computed property
        builder.Ignore(d => d.IsExpired);

        // Global query filter for soft delete
        builder.HasQueryFilter(d => !d.IsDeleted);
    }
}
