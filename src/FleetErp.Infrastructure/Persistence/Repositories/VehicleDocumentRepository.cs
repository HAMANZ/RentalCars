using FleetErp.Application.Vehicles.Interfaces;
using FleetErp.Domain.Entities.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace FleetErp.Infrastructure.Persistence.Repositories;

public class VehicleDocumentRepository : IVehicleDocumentRepository
{
    private readonly FleetErpDbContext _context;

    public VehicleDocumentRepository(FleetErpDbContext context)
    {
        _context = context;
    }

    public async Task<VehicleDocument?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.VehicleDocuments
            .Include(d => d.DocumentType)
            .FirstOrDefaultAsync(d => d.Id == id, ct);
    }

    public async Task<IReadOnlyList<VehicleDocument>> GetByVehicleIdAsync(int vehicleId, CancellationToken ct = default)
    {
        return await _context.VehicleDocuments
            .Include(d => d.DocumentType)
            .Where(d => d.VehicleId == vehicleId)
            .OrderByDescending(d => d.CreatedAt)
            .ToListAsync(ct);
    }

    public async Task AddAsync(VehicleDocument document, CancellationToken ct = default)
    {
        await _context.VehicleDocuments.AddAsync(document, ct);
    }

    public void Update(VehicleDocument document)
    {
        _context.VehicleDocuments.Update(document);
    }

    public void Delete(VehicleDocument document)
    {
        _context.VehicleDocuments.Remove(document);
    }
}
