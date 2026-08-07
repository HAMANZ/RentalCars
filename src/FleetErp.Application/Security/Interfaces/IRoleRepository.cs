using FleetErp.Application.Common;
using FleetErp.Domain.Entities.Security;

namespace FleetErp.Application.Security.Interfaces;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Role?> GetByIdWithPermissionsAsync(int id, CancellationToken ct = default);
    Task<Role?> GetByNameAsync(string name, CancellationToken ct = default);
    Task<IReadOnlyList<Role>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Role>> GetAllWithPermissionsAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Role>> GetByIdsAsync(List<int> ids, CancellationToken ct = default);
    Task<(IReadOnlyList<Role> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, CancellationToken ct = default);
    Task<bool> NameExistsAsync(string name, int? excludeRoleId = null, CancellationToken ct = default);
    Task AddAsync(Role role, CancellationToken ct = default);
    void Update(Role role);
    void Delete(Role role);
}
