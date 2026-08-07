using FleetErp.Application.Common;
using FleetErp.Application.Investors.Interfaces;
using FleetErp.Domain.Entities.Investors;
using Microsoft.EntityFrameworkCore;

namespace FleetErp.Infrastructure.Persistence.Repositories;

public class InvestorRepository : IInvestorRepository
{
    private readonly FleetErpDbContext _context;

    public InvestorRepository(FleetErpDbContext context)
    {
        _context = context;
    }

    public async Task<Investor?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.Investors
            .Include(i => i.Status)
            .FirstOrDefaultAsync(i => i.Id == id, ct);
    }

    public async Task<Investor?> GetByIdWithDocumentsAsync(int id, CancellationToken ct = default)
    {
        return await _context.Investors
            .Include(i => i.Status)
            .Include(i => i.Documents)
                .ThenInclude(d => d.DocumentType)
            .FirstOrDefaultAsync(i => i.Id == id, ct);
    }

    public async Task<PagedResult<Investor>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default)
    {
        var query = _context.Investors.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(i =>
                i.FullName.Contains(search) ||
                (i.Email != null && i.Email.Contains(search)) ||
                (i.Phone != null && i.Phone.Contains(search)) ||
                (i.NationalId != null && i.NationalId.Contains(search)));
        }

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .Include(i => i.Status)
            .OrderBy(i => i.FullName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedResult<Investor>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task<bool> EmailExistsAsync(string email, int? excludeInvestorId = null, CancellationToken ct = default)
    {
        var query = _context.Investors.Where(i => i.Email == email);
        if (excludeInvestorId.HasValue)
        {
            query = query.Where(i => i.Id != excludeInvestorId.Value);
        }
        return await query.AnyAsync(ct);
    }

    public async Task<bool> NationalIdExistsAsync(string nationalId, int? excludeInvestorId = null, CancellationToken ct = default)
    {
        var query = _context.Investors.Where(i => i.NationalId == nationalId);
        if (excludeInvestorId.HasValue)
        {
            query = query.Where(i => i.Id != excludeInvestorId.Value);
        }
        return await query.AnyAsync(ct);
    }

    public async Task AddAsync(Investor investor, CancellationToken ct = default)
    {
        await _context.Investors.AddAsync(investor, ct);
    }

    public void Update(Investor investor)
    {
        _context.Investors.Update(investor);
    }

    public void Delete(Investor investor)
    {
        _context.Investors.Remove(investor);
    }
}
