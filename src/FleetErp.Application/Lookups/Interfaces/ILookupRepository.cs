using FleetErp.Application.Common;
using FleetErp.Domain.Entities.Lookups;

namespace FleetErp.Application.Lookups.Interfaces;

/// <summary>
/// Repository for dynamic lookup items (statuses, types, categories).
/// </summary>
public interface ILookupRepository
{
    Task<LookupItem?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<LookupItem?> GetByTypeAndCodeAsync(string lookupType, string code, CancellationToken ct = default);
    Task<IReadOnlyList<LookupItem>> GetByTypeAsync(string lookupType, bool activeOnly = true, CancellationToken ct = default);
    Task<PagedResult<LookupItem>> GetPagedAsync(string? lookupType, int page, int pageSize, CancellationToken ct = default);
    Task AddAsync(LookupItem item, CancellationToken ct = default);
    void Update(LookupItem item);
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
