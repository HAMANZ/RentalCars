using FleetErp.Application.Common;
using FleetErp.Application.Customers.Interfaces;
using FleetErp.Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;

namespace FleetErp.Infrastructure.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly FleetErpDbContext _context;

    public CustomerRepository(FleetErpDbContext context)
    {
        _context = context;
    }

    public async Task<Customer?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.Customers
            .Include(c => c.Status)
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task<Customer?> GetByIdWithDocumentsAsync(int id, CancellationToken ct = default)
    {
        return await _context.Customers
            .Include(c => c.Status)
            .Include(c => c.Documents)
                .ThenInclude(d => d.DocumentType)
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task<PagedResult<Customer>> GetPagedAsync(int page, int pageSize, string? search, CancellationToken ct = default)
    {
        var query = _context.Customers
            .Include(c => c.Status)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(c =>
                c.FullName.Contains(search) ||
                (c.Email != null && c.Email.Contains(search)) ||
                (c.Phone != null && c.Phone.Contains(search)) ||
                (c.NationalId != null && c.NationalId.Contains(search)) ||
                (c.DrivingLicenseNumber != null && c.DrivingLicenseNumber.Contains(search)));
        }

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .OrderByDescending(c => c.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedResult<Customer>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task<bool> EmailExistsAsync(string email, int? excludeCustomerId = null, CancellationToken ct = default)
    {
        var normalizedEmail = email.ToLowerInvariant();
        var query = _context.Customers.Where(c => c.Email != null && c.Email.ToLower() == normalizedEmail);

        if (excludeCustomerId.HasValue)
        {
            query = query.Where(c => c.Id != excludeCustomerId.Value);
        }

        return await query.AnyAsync(ct);
    }

    public async Task<bool> NationalIdExistsAsync(string nationalId, int? excludeCustomerId = null, CancellationToken ct = default)
    {
        var query = _context.Customers.Where(c => c.NationalId == nationalId);

        if (excludeCustomerId.HasValue)
        {
            query = query.Where(c => c.Id != excludeCustomerId.Value);
        }

        return await query.AnyAsync(ct);
    }

    public async Task<bool> DrivingLicenseExistsAsync(string licenseNumber, int? excludeCustomerId = null, CancellationToken ct = default)
    {
        var query = _context.Customers.Where(c => c.DrivingLicenseNumber == licenseNumber);

        if (excludeCustomerId.HasValue)
        {
            query = query.Where(c => c.Id != excludeCustomerId.Value);
        }

        return await query.AnyAsync(ct);
    }

    public async Task AddAsync(Customer customer, CancellationToken ct = default)
    {
        await _context.Customers.AddAsync(customer, ct);
    }

    public void Update(Customer customer)
    {
        _context.Customers.Update(customer);
    }

    public void Delete(Customer customer)
    {
        customer.IsDeleted = true;
        _context.Customers.Update(customer);
    }
}
