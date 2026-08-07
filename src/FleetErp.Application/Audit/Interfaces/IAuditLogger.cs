namespace FleetErp.Application.Audit.Interfaces;

/// <summary>
/// Audit logging service for tracking all system mutations.
/// Every create/update/delete/status-change must log through this interface.
/// </summary>
public interface IAuditLogger
{
    /// <summary>
    /// Log an audit entry. Called from service-layer methods.
    /// </summary>
    /// <param name="action">Action performed (Created, Updated, Deleted, StatusChanged, etc.)</param>
    /// <param name="entityType">Type name of the entity</param>
    /// <param name="entityId">Primary key of the entity</param>
    /// <param name="userId">User who performed the action</param>
    /// <param name="details">Human-readable description</param>
    /// <param name="oldValues">Optional JSON of old values</param>
    /// <param name="newValues">Optional JSON of new values</param>
    void Log(
        string action,
        string entityType,
        int entityId,
        string? userId,
        string? details = null,
        string? oldValues = null,
        string? newValues = null);
}
