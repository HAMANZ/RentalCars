using FleetErp.Application.Common;
using FleetErp.Domain.Entities.Security;

namespace FleetErp.Application.Security.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<User?> GetByIdWithRolesAsync(int id, CancellationToken ct = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
    Task<User?> GetByEmailWithRolesAndPermissionsAsync(string email, CancellationToken ct = default);
    Task<PagedResult<User>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<bool> EmailExistsAsync(string email, int? excludeUserId = null, CancellationToken ct = default);
    Task AddAsync(User user, CancellationToken ct = default);
    void Update(User user);
    void Delete(User user);
}
