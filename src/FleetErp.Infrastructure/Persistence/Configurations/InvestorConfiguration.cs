using FleetErp.Domain.Entities.Investors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetErp.Infrastructure.Persistence.Configurations;

public class InvestorConfiguration : IEntityTypeConfiguration<Investor>
{
    public void Configure(EntityTypeBuilder<Investor> builder)
    {
        builder.ToTable("investors");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Phone)
            .HasMaxLength(50);

        builder.Property(x => x.Email)
            .HasMaxLength(150);

        builder.Property(x => x.NationalId)
            .HasMaxLength(50);

        // Indexes
        builder.HasIndex(x => x.Email);
        builder.HasIndex(x => x.NationalId);
        builder.HasIndex(x => x.StatusId);
        builder.HasIndex(x => x.AccountId);

        // Relationships
        builder.HasOne(x => x.Status)
            .WithMany()
            .HasForeignKey(x => x.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Account)
            .WithMany()
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Documents)
            .WithOne(x => x.Investor)
            .HasForeignKey(x => x.InvestorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
