using FleetErp.Application.Audit.Interfaces;
using FleetErp.Application.Common;
using FleetErp.Application.Common.Interfaces;
using FleetErp.Application.Security.Dtos;
using FleetErp.Application.Security.Interfaces;
using FleetErp.Domain.Entities.Security;
using FleetErp.Shared.Constants;
using Microsoft.Extensions.Options;

namespace FleetErp.Infrastructure.Identity;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IJwtService _jwtService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAuditLogger _auditLogger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IPermissionRepository permissionRepository,
        IJwtService jwtService,
        IPasswordHasher passwordHasher,
        IAuditLogger auditLogger,
        IUnitOfWork unitOfWork,
        IOptions<JwtSettings> jwtSettings)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _permissionRepository = permissionRepository;
        _jwtService = jwtService;
        _passwordHasher = passwordHasher;
        _auditLogger = auditLogger;
        _unitOfWork = unitOfWork;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, ct);

        if (user == null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            return Result<LoginResponse>.Invalid("Invalid email or password");
        }

        if (!user.IsActive)
        {
            return Result<LoginResponse>.Invalid("Account is deactivated");
        }

        // Get user permissions
        var permissions = await _permissionRepository.GetPermissionCodesForUserAsync(user.Id, ct);

        // Generate tokens
        var (accessToken, expiresAt) = _jwtService.GenerateAccessToken(user, permissions);
        var refreshToken = _jwtService.GenerateRefreshToken();

        // Save refresh token
        var refreshTokenEntity = new RefreshToken
        {
            UserId = user.Id,
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays)
        };
        await _refreshTokenRepository.AddAsync(refreshTokenEntity, ct);

        // Update last login
        user.LastLoginAt = DateTime.UtcNow;
        _userRepository.Update(user);

        _auditLogger.Log(AuditActions.Login, nameof(User), user.Id, user.Id.ToString(), $"User {user.Email} logged in");

        await _unitOfWork.SaveChangesAsync(ct);

        // Get roles for response
        var userWithRoles = await _userRepository.GetByIdWithRolesAsync(user.Id, ct);
        var roles = userWithRoles?.UserRoles.Select(ur => ur.Role.Name).ToList() ?? [];

        var userDto = new UserDto(
            user.Id,
            user.FullName,
            user.Email,
            user.IsActive,
            user.LastLoginAt,
            roles
        );

        return Result<LoginResponse>.Success(new LoginResponse(accessToken, refreshToken, expiresAt, userDto));
    }

    public async Task<Result<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken ct = default)
    {
        var storedToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken, ct);

        if (storedToken == null)
        {
            return Result<RefreshTokenResponse>.Invalid("Invalid refresh token");
        }

        if (!storedToken.IsActive)
        {
            return Result<RefreshTokenResponse>.Invalid("Refresh token is expired or revoked");
        }

        var user = storedToken.User;
        if (!user.IsActive)
        {
            return Result<RefreshTokenResponse>.Invalid("Account is deactivated");
        }

        // Get user permissions
        var permissions = await _permissionRepository.GetPermissionCodesForUserAsync(user.Id, ct);

        // Generate new tokens
        var (accessToken, expiresAt) = _jwtService.GenerateAccessToken(user, permissions);
        var newRefreshToken = _jwtService.GenerateRefreshToken();

        // Revoke old token and save new one
        storedToken.Revoke("Replaced by new token", newRefreshToken);
        _refreshTokenRepository.Update(storedToken);

        var newRefreshTokenEntity = new RefreshToken
        {
            UserId = user.Id,
            Token = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays)
        };
        await _refreshTokenRepository.AddAsync(newRefreshTokenEntity, ct);

        await _unitOfWork.SaveChangesAsync(ct);

        return Result<RefreshTokenResponse>.Success(new RefreshTokenResponse(accessToken, newRefreshToken, expiresAt));
    }

    public async Task<Result> LogoutAsync(string refreshToken, CancellationToken ct = default)
    {
        var storedToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken, ct);

        if (storedToken != null && storedToken.IsActive)
        {
            storedToken.Revoke("User logged out");
            _refreshTokenRepository.Update(storedToken);

            _auditLogger.Log(AuditActions.Logout, nameof(User), storedToken.UserId, storedToken.UserId.ToString(), "User logged out");

            await _unitOfWork.SaveChangesAsync(ct);
        }

        return Result.Success();
    }

    public async Task<Result> ChangePasswordAsync(int userId, ChangePasswordRequest request, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdAsync(userId, ct);
        if (user == null)
        {
            return Result.NotFound("User not found");
        }

        if (!_passwordHasher.Verify(request.CurrentPassword, user.PasswordHash))
        {
            return Result.Invalid("Current password is incorrect");
        }

        user.PasswordHash = _passwordHasher.Hash(request.NewPassword);
        _userRepository.Update(user);

        // Revoke all refresh tokens
        await _refreshTokenRepository.RevokeAllUserTokensAsync(userId, "Password changed", ct);

        _auditLogger.Log(AuditActions.PasswordChanged, nameof(User), userId, userId.ToString(), "Password changed");

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }

    public async Task<Result<UserProfileDto>> GetProfileAsync(int userId, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdWithRolesAsync(userId, ct);
        if (user == null)
        {
            return Result<UserProfileDto>.NotFound("User not found");
        }

        var permissions = await _permissionRepository.GetPermissionCodesForUserAsync(userId, ct);
        var roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();

        return Result<UserProfileDto>.Success(new UserProfileDto(
            user.Id,
            user.FullName,
            user.Email,
            roles,
            permissions.ToList()
        ));
    }
}
