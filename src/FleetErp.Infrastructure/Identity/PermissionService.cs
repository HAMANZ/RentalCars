using FleetErp.Application.Common;
using FleetErp.Application.Security.Dtos;
using FleetErp.Application.Security.Interfaces;

namespace FleetErp.Infrastructure.Identity;

public class PermissionService : IPermissionService
{
    private readonly IPermissionRepository _permissionRepository;

    public PermissionService(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    public async Task<Result<IReadOnlyList<PermissionDto>>> GetAllAsync(CancellationToken ct = default)
    {
        var permissions = await _permissionRepository.GetAllAsync(ct);
        var dtos = permissions.Select(p => new PermissionDto(
            p.Id,
            p.Code,
            p.Description,
            p.Module
        )).ToList();

        return Result<IReadOnlyList<PermissionDto>>.Success(dtos);
    }

    public async Task<Result<IReadOnlyList<PermissionGroupDto>>> GetGroupedAsync(CancellationToken ct = default)
    {
        var permissions = await _permissionRepository.GetAllAsync(ct);

        var groups = permissions
            .GroupBy(p => p.Module)
            .OrderBy(g => g.Key)
            .Select(g => new PermissionGroupDto(
                g.Key ?? "Other",
                g.Select(p => new PermissionDto(p.Id, p.Code, p.Description, p.Module)).ToList()
            ))
            .ToList();

        return Result<IReadOnlyList<PermissionGroupDto>>.Success(groups);
    }

    public async Task<Result<IReadOnlyList<PermissionDto>>> GetByRoleIdAsync(int roleId, CancellationToken ct = default)
    {
        var permissions = await _permissionRepository.GetByRoleIdAsync(roleId, ct);
        var dtos = permissions.Select(p => new PermissionDto(
            p.Id,
            p.Code,
            p.Description,
            p.Module
        )).ToList();

        return Result<IReadOnlyList<PermissionDto>>.Success(dtos);
    }
}
