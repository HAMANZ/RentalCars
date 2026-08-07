using FleetErp.Application.Common;
using FleetErp.Application.Insurance.Interfaces;
using FleetErp.Domain.Entities.Insurance;
using Microsoft.EntityFrameworkCore;

namespace FleetErp.Infrastructure.Persistence.Repositories;

public class InsuranceCompanyRepository : IInsuranceCompanyRepository
{
    private readonly FleetErpDbContext _context;

    public InsuranceCompanyRepository(FleetErpDbContext context)
    {
        _context = context;
    }

    public async Task<InsuranceCompany?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.InsuranceCompanies
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task<PagedResult<InsuranceCompany>> GetPagedAsync(int page, int pageSize, string? search, bool? activeOnly, CancellationToken ct = default)
    {
        var query = _context.InsuranceCompanies.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(c =>
                c.Name.Contains(search) ||
                (c.Phone != null && c.Phone.Contains(search)) ||
                (c.Email != null && c.Email.Contains(search)) ||
                (c.ContactPerson != null && c.ContactPerson.Contains(search)));
        }

        if (activeOnly == true)
        {
            query = query.Where(c => c.IsActive);
        }

        var totalCount = await query.CountAsync(ct);
        var items = await query
            .OrderBy(c => c.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedResult<InsuranceCompany>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task<IReadOnlyList<InsuranceCompany>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.InsuranceCompanies
            .OrderBy(c => c.Name)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<InsuranceCompany>> GetAllActiveAsync(CancellationToken ct = default)
    {
        return await _context.InsuranceCompanies
            .Where(c => c.IsActive)
            .OrderBy(c => c.Name)
            .ToListAsync(ct);
    }

    public async Task<bool> ExistsByNameAsync(string name, int? excludeId = null, CancellationToken ct = default)
    {
        var query = _context.InsuranceCompanies.Where(c => c.Name == name);

        if (excludeId.HasValue)
        {
            query = query.Where(c => c.Id != excludeId.Value);
        }

        return await query.AnyAsync(ct);
    }

    public async Task AddAsync(InsuranceCompany company, CancellationToken ct = default)
    {
        await _context.InsuranceCompanies.AddAsync(company, ct);
    }

    public void Update(InsuranceCompany company)
    {
        _context.InsuranceCompanies.Update(company);
    }

    public void Delete(InsuranceCompany company)
    {
        company.IsDeleted = true;
    }
}
