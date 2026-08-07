using FleetErp.Application.Audit.Interfaces;
using FleetErp.Application.Common.Interfaces;
using FleetErp.Domain.Entities.Audit;
using FleetErp.Infrastructure.Persistence;
using FleetErp.Infrastructure.Persistence.Repositories;

namespace FleetErp.Infrastructure.Services;

public class AuditLogger : IAuditLogger
{
    private readonly AuditLogRepository _repository;
    private readonly IDateTimeProvider _dateTime;
    private readonly ICurrentUser _currentUser;

    public AuditLogger(
        AuditLogRepository repository,
        IDateTimeProvider dateTime,
        ICurrentUser currentUser)
    {
        _repository = repository;
        _dateTime = dateTime;
        _currentUser = currentUser;
    }

    public void Log(
        string action,
        string entityType,
        int entityId,
        string? userId,
        string? details = null,
        string? oldValues = null,
        string? newValues = null)
    {
        var log = new AuditLog
        {
            Action = action,
            EntityType = entityType,
            EntityId = entityId,
            UserId = userId ?? _currentUser.UserId,
            UserName = _currentUser.UserName,
            Details = details,
            OldValues = oldValues,
            NewValues = newValues,
            Timestamp = _dateTime.UtcNow,
            CreatedAt = _dateTime.UtcNow
        };

        // Note: This is synchronous because it's called within a service method
        // that will call SaveChangesAsync at the end
        _repository.AddAsync(log).GetAwaiter().GetResult();
    }
}
