using FleetErp.Application.Common;
using FleetErp.Application.Security.Dtos;

namespace FleetErp.Application.Security.Interfaces;

public interface IRoleService
{
    Task<Result<RoleDto>> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Result<IReadOnlyList<RoleListDto>>> GetAllAsync(CancellationToken ct = default);
    Task<Result<PagedResult<RoleListDto>>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default);
    Task<Result<RoleDto>> CreateAsync(CreateRoleRequest request, CancellationToken ct = default);
    Task<Result<RoleDto>> UpdateAsync(int id, UpdateRoleRequest request, CancellationToken ct = default);
    Task<Result> DeleteAsync(int id, CancellationToken ct = default);
}
