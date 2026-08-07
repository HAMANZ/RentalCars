namespace FleetErp.Application.Common.Interfaces;

/// <summary>
/// Unit of Work interface for atomic transactions.
/// All service-layer mutations should call SaveChangesAsync at the end
/// to commit domain changes, audit logs, and accounting transactions together.
/// </summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
