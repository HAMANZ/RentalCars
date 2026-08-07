using FleetErp.Domain.Entities.Investors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetErp.Infrastructure.Persistence.Configurations;

public class InvestorDocumentConfiguration : IEntityTypeConfiguration<InvestorDocument>
{
    public void Configure(EntityTypeBuilder<InvestorDocument> builder)
    {
        builder.ToTable("investor_documents");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FilePath)
            .IsRequired()
            .HasMaxLength(500);

        // Ignore computed property
        builder.Ignore(x => x.IsExpired);

        // Indexes
        builder.HasIndex(x => x.InvestorId);
        builder.HasIndex(x => x.DocumentTypeId);

        // Relationships
        builder.HasOne(x => x.DocumentType)
            .WithMany()
            .HasForeignKey(x => x.DocumentTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
