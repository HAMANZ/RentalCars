using FleetErp.Application.Security.Interfaces;
using FleetErp.Domain.Entities.Security;
using Microsoft.EntityFrameworkCore;

namespace FleetErp.Infrastructure.Persistence.Repositories.Security;

public class PermissionRepository : IPermissionRepository
{
    private readonly FleetErpDbContext _context;

    public PermissionRepository(FleetErpDbContext context)
    {
        _context = context;
    }

    public async Task<Permission?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.Permissions.FindAsync([id], ct);
    }

    public async Task<Permission?> GetByCodeAsync(string code, CancellationToken ct = default)
    {
        return await _context.Permissions
            .FirstOrDefaultAsync(p => p.Code == code, ct);
    }

    public async Task<IReadOnlyList<Permission>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Permissions
            .OrderBy(p => p.Module)
            .ThenBy(p => p.Code)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Permission>> GetByIdsAsync(List<int> ids, CancellationToken ct = default)
    {
        return await _context.Permissions
            .Where(p => ids.Contains(p.Id))
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Permission>> GetByModuleAsync(string module, CancellationToken ct = default)
    {
        return await _context.Permissions
            .Where(p => p.Module == module)
            .OrderBy(p => p.Code)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Permission>> GetByRoleIdAsync(int roleId, CancellationToken ct = default)
    {
        return await _context.RolePermissions
            .Where(rp => rp.RoleId == roleId)
            .Select(rp => rp.Permission)
            .OrderBy(p => p.Module)
            .ThenBy(p => p.Code)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Permission>> GetPermissionsForUserAsync(int userId, CancellationToken ct = default)
    {
        return await _context.UserRoles
            .Where(ur => ur.UserId == userId)
            .SelectMany(ur => ur.Role.RolePermissions)
            .Select(rp => rp.Permission)
            .Distinct()
            .OrderBy(p => p.Module)
            .ThenBy(p => p.Code)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<string>> GetPermissionCodesForUserAsync(int userId, CancellationToken ct = default)
    {
        return await _context.UserRoles
            .Where(ur => ur.UserId == userId)
            .SelectMany(ur => ur.Role.RolePermissions)
            .Select(rp => rp.Permission.Code)
            .Distinct()
            .ToListAsync(ct);
    }

    public async Task AddAsync(Permission permission, CancellationToken ct = default)
    {
        await _context.Permissions.AddAsync(permission, ct);
    }

    public async Task AddRangeAsync(IEnumerable<Permission> permissions, CancellationToken ct = default)
    {
        await _context.Permissions.AddRangeAsync(permissions, ct);
    }
}
