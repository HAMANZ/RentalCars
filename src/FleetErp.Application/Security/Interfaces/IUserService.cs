using FleetErp.Application.Common;
using FleetErp.Application.Security.Dtos;

namespace FleetErp.Application.Security.Interfaces;

public interface IUserService
{
    Task<Result<UserDto>> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Result<PagedResult<UserDto>>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<Result<UserDto>> CreateAsync(CreateUserRequest request, CancellationToken ct = default);
    Task<Result<UserDto>> UpdateAsync(int id, UpdateUserRequest request, CancellationToken ct = default);
    Task<Result> DeleteAsync(int id, CancellationToken ct = default);
    Task<Result> ActivateAsync(int id, CancellationToken ct = default);
    Task<Result> DeactivateAsync(int id, CancellationToken ct = default);
}
