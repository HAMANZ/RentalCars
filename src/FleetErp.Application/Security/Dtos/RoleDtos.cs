namespace FleetErp.Application.Security.Dtos;

public record RoleDto(
    int Id,
    string Name,
    string? Description,
    bool IsSystem,
    IReadOnlyList<string> PermissionCodes
);

public record CreateRoleRequest(
    string Name,
    string? Description,
    IReadOnlyList<int> PermissionIds
);

public record UpdateRoleRequest(
    string Name,
    string? Description,
    IReadOnlyList<int> PermissionIds
);

public record RoleListDto(
    int Id,
    string Name,
    string? Description,
    bool IsSystem,
    int PermissionCount
);
