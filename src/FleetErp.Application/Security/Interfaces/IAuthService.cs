using FleetErp.Application.Common;
using FleetErp.Application.Security.Dtos;

namespace FleetErp.Application.Security.Interfaces;

public interface IAuthService
{
    Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken ct = default);
    Task<Result<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken ct = default);
    Task<Result> LogoutAsync(string refreshToken, CancellationToken ct = default);
    Task<Result> ChangePasswordAsync(int userId, ChangePasswordRequest request, CancellationToken ct = default);
    Task<Result<UserProfileDto>> GetProfileAsync(int userId, CancellationToken ct = default);
}
