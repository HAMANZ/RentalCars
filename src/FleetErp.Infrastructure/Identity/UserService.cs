using FleetErp.Application.Audit.Interfaces;
using FleetErp.Application.Common;
using FleetErp.Application.Common.Interfaces;
using FleetErp.Application.Security.Dtos;
using FleetErp.Application.Security.Interfaces;
using FleetErp.Domain.Entities.Security;
using FleetErp.Shared.Constants;

namespace FleetErp.Infrastructure.Identity;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAuditLogger _auditLogger;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IPasswordHasher passwordHasher,
        IAuditLogger auditLogger,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _passwordHasher = passwordHasher;
        _auditLogger = auditLogger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserDto>> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdWithRolesAsync(id, ct);
        if (user == null)
        {
            return Result<UserDto>.NotFound("User not found");
        }

        return Result<UserDto>.Success(MapToDto(user));
    }

    public async Task<Result<PagedResult<UserDto>>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default)
    {
        var pagedResult = await _userRepository.GetPagedAsync(page, pageSize, search, ct);

        var dtos = pagedResult.Items.Select(MapToDto).ToList();

        return Result<PagedResult<UserDto>>.Success(new PagedResult<UserDto>
        {
            Items = dtos,
            Page = pagedResult.Page,
            PageSize = pagedResult.PageSize,
            TotalCount = pagedResult.TotalCount
        });
    }

    public async Task<Result<UserDto>> CreateAsync(CreateUserRequest request, CancellationToken ct = default)
    {
        // Check if email already exists
        var existingUser = await _userRepository.GetByEmailAsync(request.Email, ct);
        if (existingUser != null)
        {
            return Result<UserDto>.Conflict("A user with this email already exists");
        }

        var user = new User
        {
            FullName = request.FullName,
            Email = request.Email.ToLowerInvariant(),
            PasswordHash = _passwordHasher.Hash(request.Password),
            IsActive = true
        };

        await _userRepository.AddAsync(user, ct);

        // Assign roles
        if (request.RoleIds.Count > 0)
        {
            var roles = await _roleRepository.GetByIdsAsync(request.RoleIds.ToList(), ct);
            foreach (var role in roles)
            {
                user.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = role.Id });
            }
        }

        _auditLogger.Log(AuditActions.UserCreated, nameof(User), user.Id, user.Id.ToString(), $"User {user.Email} created");

        await _unitOfWork.SaveChangesAsync(ct);

        // Reload with roles
        var createdUser = await _userRepository.GetByIdWithRolesAsync(user.Id, ct);
        return Result<UserDto>.Success(MapToDto(createdUser!));
    }

    public async Task<Result<UserDto>> UpdateAsync(int id, UpdateUserRequest request, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdWithRolesAsync(id, ct);
        if (user == null)
        {
            return Result<UserDto>.NotFound("User not found");
        }

        // Check if email is being changed and if it's already taken
        if (!user.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase))
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email, ct);
            if (existingUser != null)
            {
                return Result<UserDto>.Conflict("A user with this email already exists");
            }
        }

        user.FullName = request.FullName;
        user.Email = request.Email.ToLowerInvariant();
        user.IsActive = request.IsActive;

        // Update roles
        user.UserRoles.Clear();
        if (request.RoleIds.Count > 0)
        {
            var roles = await _roleRepository.GetByIdsAsync(request.RoleIds.ToList(), ct);
            foreach (var role in roles)
            {
                user.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = role.Id });
            }
        }

        _userRepository.Update(user);
        _auditLogger.Log(AuditActions.UserUpdated, nameof(User), user.Id, user.Id.ToString(), $"User {user.Email} updated");

        await _unitOfWork.SaveChangesAsync(ct);

        return Result<UserDto>.Success(MapToDto(user));
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdAsync(id, ct);
        if (user == null)
        {
            return Result.NotFound("User not found");
        }

        _userRepository.Delete(user);
        _auditLogger.Log(AuditActions.UserDeleted, nameof(User), user.Id, user.Id.ToString(), $"User {user.Email} deleted");

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }

    public async Task<Result> ActivateAsync(int id, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdAsync(id, ct);
        if (user == null)
        {
            return Result.NotFound("User not found");
        }

        user.IsActive = true;
        _userRepository.Update(user);

        _auditLogger.Log(AuditActions.UserActivated, nameof(User), user.Id, user.Id.ToString(), $"User {user.Email} activated");

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }

    public async Task<Result> DeactivateAsync(int id, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdAsync(id, ct);
        if (user == null)
        {
            return Result.NotFound("User not found");
        }

        user.IsActive = false;
        _userRepository.Update(user);

        _auditLogger.Log(AuditActions.UserDeactivated, nameof(User), user.Id, user.Id.ToString(), $"User {user.Email} deactivated");

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }

    private static UserDto MapToDto(User user)
    {
        var roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();
        return new UserDto(
            user.Id,
            user.FullName,
            user.Email,
            user.IsActive,
            user.LastLoginAt,
            roles
        );
    }
}
