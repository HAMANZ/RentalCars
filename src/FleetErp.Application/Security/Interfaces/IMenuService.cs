using FleetErp.Application.Common;
using FleetErp.Application.Security.Dtos;

namespace FleetErp.Application.Security.Interfaces;

public interface IMenuService
{
    Task<Result<IReadOnlyList<MenuDto>>> GetMenusForCurrentUserAsync(int userId, CancellationToken ct = default);
    Task<Result<IReadOnlyList<MenuDto>>> GetAllMenusAsync(CancellationToken ct = default);
}
