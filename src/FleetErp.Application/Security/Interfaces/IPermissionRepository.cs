using FleetErp.Domain.Entities.Security;

namespace FleetErp.Application.Security.Interfaces;

public interface IPermissionRepository
{
    Task<Permission?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Permission?> GetByCodeAsync(string code, CancellationToken ct = default);
    Task<IReadOnlyList<Permission>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Permission>> GetByIdsAsync(List<int> ids, CancellationToken ct = default);
    Task<IReadOnlyList<Permission>> GetByModuleAsync(string module, CancellationToken ct = default);
    Task<IReadOnlyList<Permission>> GetByRoleIdAsync(int roleId, CancellationToken ct = default);
    Task<IReadOnlyList<Permission>> GetPermissionsForUserAsync(int userId, CancellationToken ct = default);
    Task<IReadOnlyList<string>> GetPermissionCodesForUserAsync(int userId, CancellationToken ct = default);
    Task AddAsync(Permission permission, CancellationToken ct = default);
    Task AddRangeAsync(IEnumerable<Permission> permissions, CancellationToken ct = default);
}
