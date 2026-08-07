namespace FleetErp.Application.Security.Dtos;

public record MenuDto(
    int Id,
    string Name,
    string? Route,
    string? Icon,
    int SortOrder,
    IReadOnlyList<MenuDto> Children
);

public record CreateMenuRequest(
    int? ParentId,
    string Name,
    string? Route,
    string? Icon,
    int SortOrder,
    int? PermissionId
);

public record UpdateMenuRequest(
    int? ParentId,
    string Name,
    string? Route,
    string? Icon,
    int SortOrder,
    int? PermissionId,
    bool IsActive
);
