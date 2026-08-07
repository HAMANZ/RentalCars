using FleetErp.Domain.Entities.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetErp.Infrastructure.Persistence.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("notifications");

        builder.HasKey(n => n.Id);

        builder.Property(n => n.Id)
            .HasColumnName("id");

        builder.Property(n => n.NotificationTypeId)
            .HasColumnName("notification_type_id")
            .IsRequired();

        builder.Property(n => n.StatusId)
            .HasColumnName("status_id")
            .IsRequired();

        builder.Property(n => n.Title)
            .HasColumnName("title")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(n => n.Message)
            .HasColumnName("message")
            .HasMaxLength(500);

        builder.Property(n => n.ReferenceType)
            .HasColumnName("reference_type")
            .HasMaxLength(50);

        builder.Property(n => n.ReferenceId)
            .HasColumnName("reference_id");

        builder.Property(n => n.DueAt)
            .HasColumnName("due_at");

        builder.Property(n => n.ReadAt)
            .HasColumnName("read_at");

        builder.Property(n => n.ReadByUserId)
            .HasColumnName("read_by_user_id");

        builder.Property(n => n.DismissedAt)
            .HasColumnName("dismissed_at");

        builder.Property(n => n.DismissedByUserId)
            .HasColumnName("dismissed_by_user_id");

        builder.Property(n => n.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(n => n.CreatedBy)
            .HasColumnName("created_by")
            .HasMaxLength(100);

        builder.Property(n => n.UpdatedAt)
            .HasColumnName("updated_at");

        builder.Property(n => n.UpdatedBy)
            .HasColumnName("updated_by")
            .HasMaxLength(100);

        builder.Property(n => n.IsDeleted)
            .HasColumnName("is_deleted")
            .HasDefaultValue(false);

        // Relationships
        builder.HasOne(n => n.NotificationType)
            .WithMany()
            .HasForeignKey(n => n.NotificationTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(n => n.Status)
            .WithMany()
            .HasForeignKey(n => n.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(n => n.StatusId);
        builder.HasIndex(n => n.NotificationTypeId);
        builder.HasIndex(n => new { n.ReferenceType, n.ReferenceId });
        builder.HasIndex(n => n.DueAt);

        // Global query filter for soft delete
        builder.HasQueryFilter(n => !n.IsDeleted);
    }
}
