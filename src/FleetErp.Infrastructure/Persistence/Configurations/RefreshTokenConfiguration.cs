using FleetErp.Domain.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetErp.Infrastructure.Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("refresh_tokens");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Token)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.RevokedReason)
            .HasMaxLength(255);

        builder.Property(x => x.ReplacedByToken)
            .HasMaxLength(255);

        builder.HasIndex(x => x.Token);
        builder.HasIndex(x => x.UserId);
    }
}
