using FleetErp.Application.Common;
using FleetErp.Application.Security.Dtos;
using FleetErp.Application.Security.Interfaces;

namespace FleetErp.Infrastructure.Identity;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _menuRepository;
    private readonly IPermissionRepository _permissionRepository;

    public MenuService(
        IMenuRepository menuRepository,
        IPermissionRepository permissionRepository)
    {
        _menuRepository = menuRepository;
        _permissionRepository = permissionRepository;
    }

    public async Task<Result<IReadOnlyList<MenuDto>>> GetMenusForCurrentUserAsync(int userId, CancellationToken ct = default)
    {
        // Get user's permission IDs
        var userPermissions = await _permissionRepository.GetPermissionsForUserAsync(userId, ct);
        var permissionIds = userPermissions.Select(p => p.Id).ToHashSet();

        // Get all menus
        var allMenus = await _menuRepository.GetAllWithChildrenAsync(ct);

        // Filter menus based on permissions (only top-level menus)
        var accessibleMenus = allMenus
            .Where(m => m.ParentId == null)
            .Where(m => m.PermissionId == null || permissionIds.Contains(m.PermissionId.Value))
            .OrderBy(m => m.SortOrder)
            .Select(m => MapToDto(m, permissionIds))
            .ToList();

        return Result<IReadOnlyList<MenuDto>>.Success(accessibleMenus);
    }

    public async Task<Result<IReadOnlyList<MenuDto>>> GetAllMenusAsync(CancellationToken ct = default)
    {
        var menus = await _menuRepository.GetAllWithChildrenAsync(ct);

        var topLevelMenus = menus
            .Where(m => m.ParentId == null)
            .OrderBy(m => m.SortOrder)
            .Select(MapToDtoFull)
            .ToList();

        return Result<IReadOnlyList<MenuDto>>.Success(topLevelMenus);
    }

    private static MenuDto MapToDto(Domain.Entities.Security.Menu menu, HashSet<int> userPermissionIds)
    {
        var accessibleChildren = menu.Children
            .Where(c => c.PermissionId == null || userPermissionIds.Contains(c.PermissionId.Value))
            .OrderBy(c => c.SortOrder)
            .Select(c => MapToDto(c, userPermissionIds))
            .ToList();

        return new MenuDto(
            menu.Id,
            menu.Name,
            menu.Route,
            menu.Icon,
            menu.SortOrder,
            accessibleChildren
        );
    }

    private static MenuDto MapToDtoFull(Domain.Entities.Security.Menu menu)
    {
        var children = menu.Children
            .OrderBy(c => c.SortOrder)
            .Select(MapToDtoFull)
            .ToList();

        return new MenuDto(
            menu.Id,
            menu.Name,
            menu.Route,
            menu.Icon,
            menu.SortOrder,
            children
        );
    }
}
