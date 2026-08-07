using FleetErp.Application.Insurance.Interfaces;
using FleetErp.Domain.Entities.Insurance;
using Microsoft.EntityFrameworkCore;

namespace FleetErp.Infrastructure.Persistence.Repositories;

public class InsuranceDocumentRepository : IInsuranceDocumentRepository
{
    private readonly FleetErpDbContext _context;

    public InsuranceDocumentRepository(FleetErpDbContext context)
    {
        _context = context;
    }

    public async Task<InsuranceDocument?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.InsuranceDocuments
            .Include(d => d.DocumentType)
            .FirstOrDefaultAsync(d => d.Id == id, ct);
    }

    public async Task<IReadOnlyList<InsuranceDocument>> GetByInsuranceRecordIdAsync(int insuranceRecordId, CancellationToken ct = default)
    {
        return await _context.InsuranceDocuments
            .Include(d => d.DocumentType)
            .Where(d => d.InsuranceRecordId == insuranceRecordId)
            .OrderByDescending(d => d.CreatedAt)
            .ToListAsync(ct);
    }

    public async Task AddAsync(InsuranceDocument document, CancellationToken ct = default)
    {
        await _context.InsuranceDocuments.AddAsync(document, ct);
    }

    public void Delete(InsuranceDocument document)
    {
        document.IsDeleted = true;
    }
}
