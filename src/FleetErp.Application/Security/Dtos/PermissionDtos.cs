namespace FleetErp.Application.Security.Dtos;

public record PermissionDto(
    int Id,
    string Code,
    string? Description,
    string? Module
);

public record PermissionGroupDto(
    string Module,
    IReadOnlyList<PermissionDto> Permissions
);
