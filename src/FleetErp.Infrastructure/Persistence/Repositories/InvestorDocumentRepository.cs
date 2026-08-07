using FleetErp.Application.Investors.Interfaces;
using FleetErp.Domain.Entities.Investors;
using Microsoft.EntityFrameworkCore;

namespace FleetErp.Infrastructure.Persistence.Repositories;

public class InvestorDocumentRepository : IInvestorDocumentRepository
{
    private readonly FleetErpDbContext _context;

    public InvestorDocumentRepository(FleetErpDbContext context)
    {
        _context = context;
    }

    public async Task<InvestorDocument?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.InvestorDocuments
            .Include(d => d.DocumentType)
            .FirstOrDefaultAsync(d => d.Id == id, ct);
    }

    public async Task<IReadOnlyList<InvestorDocument>> GetByInvestorIdAsync(int investorId, CancellationToken ct = default)
    {
        return await _context.InvestorDocuments
            .Include(d => d.DocumentType)
            .Where(d => d.InvestorId == investorId)
            .OrderByDescending(d => d.CreatedAt)
            .ToListAsync(ct);
    }

    public async Task AddAsync(InvestorDocument document, CancellationToken ct = default)
    {
        await _context.InvestorDocuments.AddAsync(document, ct);
    }

    public void Update(InvestorDocument document)
    {
        _context.InvestorDocuments.Update(document);
    }

    public void Delete(InvestorDocument document)
    {
        _context.InvestorDocuments.Remove(document);
    }
}
