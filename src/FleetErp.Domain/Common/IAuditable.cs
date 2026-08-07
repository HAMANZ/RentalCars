namespace FleetErp.Domain.Common;

/// <summary>
/// Marker interface for entities that require audit logging on mutations.
/// </summary>
public interface IAuditable
{
    int Id { get; }
    DateTime CreatedAt { get; set; }
    string? CreatedBy { get; set; }
    DateTime? UpdatedAt { get; set; }
    string? UpdatedBy { get; set; }
}
