using FleetErp.Application.Customers.Interfaces;
using FleetErp.Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;

namespace FleetErp.Infrastructure.Persistence.Repositories;

public class CustomerDocumentRepository : ICustomerDocumentRepository
{
    private readonly FleetErpDbContext _context;

    public CustomerDocumentRepository(FleetErpDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerDocument?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.CustomerDocuments
            .Include(d => d.DocumentType)
            .FirstOrDefaultAsync(d => d.Id == id, ct);
    }

    public async Task<IReadOnlyList<CustomerDocument>> GetByCustomerIdAsync(int customerId, CancellationToken ct = default)
    {
        return await _context.CustomerDocuments
            .Include(d => d.DocumentType)
            .Where(d => d.CustomerId == customerId)
            .OrderByDescending(d => d.CreatedAt)
            .ToListAsync(ct);
    }

    public async Task AddAsync(CustomerDocument document, CancellationToken ct = default)
    {
        await _context.CustomerDocuments.AddAsync(document, ct);
    }

    public void Delete(CustomerDocument document)
    {
        document.IsDeleted = true;
        _context.CustomerDocuments.Update(document);
    }
}
