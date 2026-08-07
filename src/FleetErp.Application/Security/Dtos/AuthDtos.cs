namespace FleetErp.Application.Security.Dtos;

public record LoginRequest(string Email, string Password);

public record LoginResponse(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt,
    UserDto User
);

public record RefreshTokenRequest(string RefreshToken);

public record RefreshTokenResponse(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt
);

public record ChangePasswordRequest(string CurrentPassword, string NewPassword, string ConfirmPassword);

public record ResetPasswordRequest(string Email);

public record LogoutRequest(string RefreshToken);
