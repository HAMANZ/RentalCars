using FleetErp.Application.Security.Interfaces;
using FleetErp.Domain.Entities.Security;
using Microsoft.EntityFrameworkCore;

namespace FleetErp.Infrastructure.Persistence.Repositories.Security;

public class RoleRepository : IRoleRepository
{
    private readonly FleetErpDbContext _context;

    public RoleRepository(FleetErpDbContext context)
    {
        _context = context;
    }

    public async Task<Role?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.Roles.FindAsync([id], ct);
    }

    public async Task<Role?> GetByIdWithPermissionsAsync(int id, CancellationToken ct = default)
    {
        return await _context.Roles
            .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(r => r.Id == id, ct);
    }

    public async Task<Role?> GetByNameAsync(string name, CancellationToken ct = default)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(r => r.Name == name, ct);
    }

    public async Task<IReadOnlyList<Role>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Roles
            .OrderBy(r => r.Name)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Role>> GetAllWithPermissionsAsync(CancellationToken ct = default)
    {
        return await _context.Roles
            .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
            .OrderBy(r => r.Name)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Role>> GetByIdsAsync(List<int> ids, CancellationToken ct = default)
    {
        return await _context.Roles
            .Where(r => ids.Contains(r.Id))
            .ToListAsync(ct);
    }

    public async Task<(IReadOnlyList<Role> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, CancellationToken ct = default)
    {
        var totalCount = await _context.Roles.CountAsync(ct);

        var items = await _context.Roles
            .Include(r => r.RolePermissions)
            .OrderBy(r => r.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return (items, totalCount);
    }

    public async Task<bool> NameExistsAsync(string name, int? excludeRoleId = null, CancellationToken ct = default)
    {
        var query = _context.Roles.Where(r => r.Name == name);
        if (excludeRoleId.HasValue)
        {
            query = query.Where(r => r.Id != excludeRoleId.Value);
        }
        return await query.AnyAsync(ct);
    }

    public async Task AddAsync(Role role, CancellationToken ct = default)
    {
        await _context.Roles.AddAsync(role, ct);
    }

    public void Update(Role role)
    {
        _context.Roles.Update(role);
    }

    public void Delete(Role role)
    {
        _context.Roles.Remove(role);
    }
}
