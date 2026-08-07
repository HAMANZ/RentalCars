using FleetErp.Application.Common;
using FleetErp.Application.Security.Dtos;

namespace FleetErp.Application.Security.Interfaces;

public interface IPermissionService
{
    Task<Result<IReadOnlyList<PermissionDto>>> GetAllAsync(CancellationToken ct = default);
    Task<Result<IReadOnlyList<PermissionGroupDto>>> GetGroupedAsync(CancellationToken ct = default);
    Task<Result<IReadOnlyList<PermissionDto>>> GetByRoleIdAsync(int roleId, CancellationToken ct = default);
}
