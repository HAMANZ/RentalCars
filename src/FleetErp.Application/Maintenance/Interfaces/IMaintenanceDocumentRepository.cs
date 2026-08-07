using FleetErp.Domain.Entities.Maintenance;

namespace FleetErp.Application.Maintenance.Interfaces;

public interface IMaintenanceDocumentRepository
{
    Task<MaintenanceDocument?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<MaintenanceDocument>> GetByMaintenanceRecordIdAsync(int recordId, CancellationToken ct = default);
    Task AddAsync(MaintenanceDocument document, CancellationToken ct = default);
    void Delete(MaintenanceDocument document);
}
