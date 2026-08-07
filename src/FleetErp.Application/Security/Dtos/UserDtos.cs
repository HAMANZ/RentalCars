namespace FleetErp.Application.Security.Dtos;

public record UserDto(
    int Id,
    string FullName,
    string Email,
    bool IsActive,
    DateTime? LastLoginAt,
    IReadOnlyList<string> Roles
);

public record CreateUserRequest(
    string FullName,
    string Email,
    string Password,
    bool IsActive,
    IReadOnlyList<int> RoleIds
);

public record UpdateUserRequest(
    string FullName,
    string Email,
    bool IsActive,
    IReadOnlyList<int> RoleIds
);

public record UserProfileDto(
    int Id,
    string FullName,
    string Email,
    IReadOnlyList<string> Roles,
    IReadOnlyList<string> Permissions
);
