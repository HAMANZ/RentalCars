using FleetErp.Application.Security.Interfaces;
using FleetErp.Domain.Entities.Security;
using Microsoft.EntityFrameworkCore;

namespace FleetErp.Infrastructure.Persistence.Repositories.Security;

public class MenuRepository : IMenuRepository
{
    private readonly FleetErpDbContext _context;

    public MenuRepository(FleetErpDbContext context)
    {
        _context = context;
    }

    public async Task<Menu?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.Menus.FindAsync([id], ct);
    }

    public async Task<IReadOnlyList<Menu>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Menus
            .Include(m => m.Permission)
            .Include(m => m.Children)
            .Where(m => m.IsActive)
            .OrderBy(m => m.SortOrder)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Menu>> GetAllWithChildrenAsync(CancellationToken ct = default)
    {
        return await _context.Menus
            .Include(m => m.Permission)
            .Include(m => m.Children.Where(c => c.IsActive).OrderBy(c => c.SortOrder))
                .ThenInclude(c => c.Permission)
            .Where(m => m.IsActive)
            .OrderBy(m => m.SortOrder)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Menu>> GetRootMenusAsync(CancellationToken ct = default)
    {
        return await _context.Menus
            .Include(m => m.Permission)
            .Include(m => m.Children.Where(c => c.IsActive).OrderBy(c => c.SortOrder))
                .ThenInclude(c => c.Permission)
            .Where(m => m.ParentId == null && m.IsActive)
            .OrderBy(m => m.SortOrder)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Menu>> GetMenusForUserAsync(int userId, CancellationToken ct = default)
    {
        // Get user's permission codes
        var userPermissionIds = await _context.UserRoles
            .Where(ur => ur.UserId == userId)
            .SelectMany(ur => ur.Role.RolePermissions)
            .Select(rp => rp.PermissionId)
            .Distinct()
            .ToListAsync(ct);

        // Get menus where either no permission is required or user has the permission
        return await _context.Menus
            .Include(m => m.Children.Where(c => c.IsActive && (c.PermissionId == null || userPermissionIds.Contains(c.PermissionId.Value))).OrderBy(c => c.SortOrder))
            .Where(m => m.ParentId == null && m.IsActive && (m.PermissionId == null || userPermissionIds.Contains(m.PermissionId.Value)))
            .OrderBy(m => m.SortOrder)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Menu menu, CancellationToken ct = default)
    {
        await _context.Menus.AddAsync(menu, ct);
    }

    public void Update(Menu menu)
    {
        _context.Menus.Update(menu);
    }
}
