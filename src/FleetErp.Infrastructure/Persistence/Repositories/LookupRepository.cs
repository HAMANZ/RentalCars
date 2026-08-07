using FleetErp.Application.Common;
using FleetErp.Application.Lookups.Interfaces;
using FleetErp.Domain.Entities.Lookups;
using Microsoft.EntityFrameworkCore;

namespace FleetErp.Infrastructure.Persistence.Repositories;

public class LookupRepository : ILookupRepository
{
    private readonly FleetErpDbContext _context;

    public LookupRepository(FleetErpDbContext context)
    {
        _context = context;
    }

    public async Task<LookupItem?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.LookupItems.FindAsync([id], ct);
    }

    public async Task<LookupItem?> GetByTypeAndCodeAsync(string lookupType, string code, CancellationToken ct = default)
    {
        return await _context.LookupItems
            .FirstOrDefaultAsync(x => x.LookupType == lookupType && x.Code == code, ct);
    }

    public async Task<IReadOnlyList<LookupItem>> GetByTypeAsync(string lookupType, bool activeOnly = true, CancellationToken ct = default)
    {
        var query = _context.LookupItems.Where(x => x.LookupType == lookupType);

        if (activeOnly)
            query = query.Where(x => x.IsActive);

        return await query.OrderBy(x => x.SortOrder).ThenBy(x => x.Name).ToListAsync(ct);
    }

    public async Task<PagedResult<LookupItem>> GetPagedAsync(string? lookupType, int page, int pageSize, CancellationToken ct = default)
    {
        var query = _context.LookupItems.AsQueryable();

        if (!string.IsNullOrEmpty(lookupType))
            query = query.Where(x => x.LookupType == lookupType);

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .OrderBy(x => x.LookupType)
            .ThenBy(x => x.SortOrder)
            .ThenBy(x => x.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedResult<LookupItem>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task AddAsync(LookupItem item, CancellationToken ct = default)
    {
        await _context.LookupItems.AddAsync(item, ct);
    }

    public void Update(LookupItem item)
    {
        _context.LookupItems.Update(item);
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await _context.SaveChangesAsync(ct);
    }
}
