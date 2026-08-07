using FleetErp.Application.Audit.Interfaces;
using FleetErp.Application.Common;
using FleetErp.Application.Common.Interfaces;
using FleetErp.Application.Security.Dtos;
using FleetErp.Application.Security.Interfaces;
using FleetErp.Domain.Entities.Security;
using FleetErp.Shared.Constants;

namespace FleetErp.Infrastructure.Identity;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IAuditLogger _auditLogger;
    private readonly IUnitOfWork _unitOfWork;

    public RoleService(
        IRoleRepository roleRepository,
        IPermissionRepository permissionRepository,
        IAuditLogger auditLogger,
        IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
        _auditLogger = auditLogger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RoleDto>> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var role = await _roleRepository.GetByIdWithPermissionsAsync(id, ct);
        if (role == null)
        {
            return Result<RoleDto>.NotFound("Role not found");
        }

        return Result<RoleDto>.Success(MapToDto(role));
    }

    public async Task<Result<IReadOnlyList<RoleListDto>>> GetAllAsync(CancellationToken ct = default)
    {
        var roles = await _roleRepository.GetAllWithPermissionsAsync(ct);
        var dtos = roles.Select(MapToListDto).ToList();
        return Result<IReadOnlyList<RoleListDto>>.Success(dtos);
    }

    public async Task<Result<PagedResult<RoleListDto>>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default)
    {
        var (roles, totalCount) = await _roleRepository.GetPagedAsync(page, pageSize, ct);
        var dtos = roles.Select(MapToListDto).ToList();
        return Result<PagedResult<RoleListDto>>.Success(new PagedResult<RoleListDto>
        {
            Items = dtos,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        });
    }

    public async Task<Result<RoleDto>> CreateAsync(CreateRoleRequest request, CancellationToken ct = default)
    {
        // Check if role name already exists
        var existingRole = await _roleRepository.GetByNameAsync(request.Name, ct);
        if (existingRole != null)
        {
            return Result<RoleDto>.Conflict("A role with this name already exists");
        }

        var role = new Role
        {
            Name = request.Name,
            Description = request.Description
        };

        await _roleRepository.AddAsync(role, ct);

        // Assign permissions
        if (request.PermissionIds.Count > 0)
        {
            var permissions = await _permissionRepository.GetByIdsAsync(request.PermissionIds.ToList(), ct);
            foreach (var permission in permissions)
            {
                role.RolePermissions.Add(new RolePermission { RoleId = role.Id, PermissionId = permission.Id });
            }
        }

        _auditLogger.Log(AuditActions.RoleCreated, nameof(Role), role.Id, role.Id.ToString(), $"Role {role.Name} created");

        await _unitOfWork.SaveChangesAsync(ct);

        // Reload with permissions
        var createdRole = await _roleRepository.GetByIdWithPermissionsAsync(role.Id, ct);
        return Result<RoleDto>.Success(MapToDto(createdRole!));
    }

    public async Task<Result<RoleDto>> UpdateAsync(int id, UpdateRoleRequest request, CancellationToken ct = default)
    {
        var role = await _roleRepository.GetByIdWithPermissionsAsync(id, ct);
        if (role == null)
        {
            return Result<RoleDto>.NotFound("Role not found");
        }

        // Prevent modifying system roles
        if (role.IsSystem)
        {
            return Result<RoleDto>.Invalid("Cannot modify system roles");
        }

        // Check if name is being changed and if it's already taken
        if (!role.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase))
        {
            var existingRole = await _roleRepository.GetByNameAsync(request.Name, ct);
            if (existingRole != null)
            {
                return Result<RoleDto>.Conflict("A role with this name already exists");
            }
        }

        role.Name = request.Name;
        role.Description = request.Description;

        // Update permissions
        role.RolePermissions.Clear();
        if (request.PermissionIds.Count > 0)
        {
            var permissions = await _permissionRepository.GetByIdsAsync(request.PermissionIds.ToList(), ct);
            foreach (var permission in permissions)
            {
                role.RolePermissions.Add(new RolePermission { RoleId = role.Id, PermissionId = permission.Id });
            }
        }

        _roleRepository.Update(role);
        _auditLogger.Log(AuditActions.RoleUpdated, nameof(Role), role.Id, role.Id.ToString(), $"Role {role.Name} updated");

        await _unitOfWork.SaveChangesAsync(ct);

        return Result<RoleDto>.Success(MapToDto(role));
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken ct = default)
    {
        var role = await _roleRepository.GetByIdAsync(id, ct);
        if (role == null)
        {
            return Result.NotFound("Role not found");
        }

        // Prevent deleting system roles
        if (role.IsSystem)
        {
            return Result.Invalid("Cannot delete system roles");
        }

        _roleRepository.Delete(role);
        _auditLogger.Log(AuditActions.RoleDeleted, nameof(Role), role.Id, role.Id.ToString(), $"Role {role.Name} deleted");

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }

    private static RoleDto MapToDto(Role role)
    {
        var permissions = role.RolePermissions.Select(rp => rp.Permission.Code).ToList();
        return new RoleDto(
            role.Id,
            role.Name,
            role.Description,
            role.IsSystem,
            permissions
        );
    }

    private static RoleListDto MapToListDto(Role role)
    {
        return new RoleListDto(
            role.Id,
            role.Name,
            role.Description,
            role.IsSystem,
            role.RolePermissions.Count
        );
    }
}
