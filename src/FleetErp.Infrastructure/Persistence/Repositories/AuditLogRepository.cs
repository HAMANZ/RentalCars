using FleetErp.Domain.Entities.Audit;
using Microsoft.EntityFrameworkCore;

namespace FleetErp.Infrastructure.Persistence.Repositories;

public class AuditLogRepository
{
    private readonly FleetErpDbContext _context;

    public AuditLogRepository(FleetErpDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(AuditLog log, CancellationToken ct = default)
    {
        await _context.AuditLogs.AddAsync(log, ct);
    }

    public async Task<IReadOnlyList<AuditLog>> GetByEntityAsync(
        string entityType,
        int entityId,
        CancellationToken ct = default)
    {
        return await _context.AuditLogs
            .IgnoreQueryFilters() // Audit logs should never be soft-deleted
            .Where(x => x.EntityType == entityType && x.EntityId == entityId)
            .OrderByDescending(x => x.Timestamp)
            .ToListAsync(ct);
    }
}
