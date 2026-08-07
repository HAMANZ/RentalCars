namespace FleetErp.Domain.Common;

/// <summary>
/// Marker interface for entities that support soft delete.
/// EF Core global query filter will exclude IsDeleted=true rows.
/// </summary>
public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}
