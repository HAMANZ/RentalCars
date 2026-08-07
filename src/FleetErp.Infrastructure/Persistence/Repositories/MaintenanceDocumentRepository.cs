using FleetErp.Application.Maintenance.Interfaces;
using FleetErp.Domain.Entities.Maintenance;
using Microsoft.EntityFrameworkCore;

namespace FleetErp.Infrastructure.Persistence.Repositories;

public class MaintenanceDocumentRepository : IMaintenanceDocumentRepository
{
    private readonly FleetErpDbContext _context;

    public MaintenanceDocumentRepository(FleetErpDbContext context)
    {
        _context = context;
    }

    public async Task<MaintenanceDocument?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.MaintenanceDocuments
            .Include(d => d.DocumentType)
            .FirstOrDefaultAsync(d => d.Id == id, ct);
    }

    public async Task<IReadOnlyList<MaintenanceDocument>> GetByMaintenanceRecordIdAsync(int recordId, CancellationToken ct = default)
    {
        return await _context.MaintenanceDocuments
            .Include(d => d.DocumentType)
            .Where(d => d.MaintenanceRecordId == recordId)
            .OrderByDescending(d => d.CreatedAt)
            .ToListAsync(ct);
    }

    public async Task AddAsync(MaintenanceDocument document, CancellationToken ct = default)
    {
        await _context.MaintenanceDocuments.AddAsync(document, ct);
    }

    public void Delete(MaintenanceDocument document)
    {
        document.IsDeleted = true;
        _context.MaintenanceDocuments.Update(document);
    }
}
