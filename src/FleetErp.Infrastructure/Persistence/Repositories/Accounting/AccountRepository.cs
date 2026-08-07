using FleetErp.Application.Accounting.Interfaces;
using FleetErp.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;

namespace FleetErp.Infrastructure.Persistence.Repositories.Accounting;

public class AccountRepository : IAccountRepository
{
    private readonly FleetErpDbContext _context;

    public AccountRepository(FleetErpDbContext context)
    {
        _context = context;
    }

    public async Task<Account?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.Accounts
            .Include(a => a.AccountType)
            .FirstOrDefaultAsync(a => a.Id == id, ct);
    }

    public async Task<Account?> GetByCodeAsync(string code, CancellationToken ct = default)
    {
        return await _context.Accounts
            .Include(a => a.AccountType)
            .FirstOrDefaultAsync(a => a.Code == code, ct);
    }

    public async Task<Account?> GetByOwnerAsync(string ownerType, int ownerId, CancellationToken ct = default)
    {
        return await _context.Accounts
            .Include(a => a.AccountType)
            .FirstOrDefaultAsync(a => a.OwnerType == ownerType && a.OwnerId == ownerId, ct);
    }

    public async Task<IReadOnlyList<Account>> GetByOwnerTypeAsync(string ownerType, CancellationToken ct = default)
    {
        return await _context.Accounts
            .Include(a => a.AccountType)
            .Where(a => a.OwnerType == ownerType)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Account>> GetSystemAccountsAsync(CancellationToken ct = default)
    {
        // System accounts are those without an OwnerId (Company, Cashbox, Bank)
        return await _context.Accounts
            .Include(a => a.AccountType)
            .Where(a => a.OwnerId == null)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Account account, CancellationToken ct = default)
    {
        await _context.Accounts.AddAsync(account, ct);
    }

    public void Update(Account account)
    {
        _context.Accounts.Update(account);
    }
}
