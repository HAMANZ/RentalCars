using FleetErp.Domain.Entities.Security;

namespace FleetErp.Application.Security.Interfaces;

public interface IMenuRepository
{
    Task<Menu?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<Menu>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Menu>> GetAllWithChildrenAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Menu>> GetRootMenusAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Menu>> GetMenusForUserAsync(int userId, CancellationToken ct = default);
    Task AddAsync(Menu menu, CancellationToken ct = default);
    void Update(Menu menu);
}
