using FleetErp.Application.Accounting.Interfaces;
using FleetErp.Application.Common;
using FleetErp.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;

namespace FleetErp.Infrastructure.Persistence.Repositories.Accounting;

public class TransactionRepository : ITransactionRepository
{
    private readonly FleetErpDbContext _context;

    public TransactionRepository(FleetErpDbContext context)
    {
        _context = context;
    }

    public async Task<Transaction?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.Transactions
            .Include(t => t.TransactionType)
            .Include(t => t.DebitAccount)
            .Include(t => t.CreditAccount)
            .FirstOrDefaultAsync(t => t.Id == id, ct);
    }

    public async Task<IReadOnlyList<Transaction>> GetByAccountIdAsync(int accountId, CancellationToken ct = default)
    {
        return await _context.Transactions
            .Include(t => t.TransactionType)
            .Include(t => t.DebitAccount)
            .Include(t => t.CreditAccount)
            .Where(t => t.DebitAccountId == accountId || t.CreditAccountId == accountId)
            .OrderByDescending(t => t.OccurredAt)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Transaction>> GetByReferenceAsync(string referenceType, int referenceId, CancellationToken ct = default)
    {
        return await _context.Transactions
            .Include(t => t.TransactionType)
            .Include(t => t.DebitAccount)
            .Include(t => t.CreditAccount)
            .Where(t => t.ReferenceType == referenceType && t.ReferenceId == referenceId)
            .OrderByDescending(t => t.OccurredAt)
            .ToListAsync(ct);
    }

    public async Task<PagedResult<Transaction>> GetPagedByAccountAsync(int accountId, int page, int pageSize, DateTime? fromDate, DateTime? toDate, CancellationToken ct = default)
    {
        var query = _context.Transactions
            .Include(t => t.TransactionType)
            .Include(t => t.DebitAccount)
            .Include(t => t.CreditAccount)
            .Where(t => t.DebitAccountId == accountId || t.CreditAccountId == accountId);

        if (fromDate.HasValue)
        {
            query = query.Where(t => t.OccurredAt >= fromDate.Value);
        }

        if (toDate.HasValue)
        {
            query = query.Where(t => t.OccurredAt <= toDate.Value);
        }

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .OrderByDescending(t => t.OccurredAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedResult<Transaction>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task<decimal> GetAccountBalanceAsync(int accountId, CancellationToken ct = default)
    {
        // Balance = Sum of debits - Sum of credits for this account
        var debits = await _context.Transactions
            .Where(t => t.DebitAccountId == accountId)
            .SumAsync(t => t.Amount, ct);

        var credits = await _context.Transactions
            .Where(t => t.CreditAccountId == accountId)
            .SumAsync(t => t.Amount, ct);

        return debits - credits;
    }

    public async Task<decimal> GetAccountBalanceAsOfAsync(int accountId, DateTime asOfDate, CancellationToken ct = default)
    {
        // Balance as of a specific date
        var debits = await _context.Transactions
            .Where(t => t.DebitAccountId == accountId && t.OccurredAt <= asOfDate)
            .SumAsync(t => t.Amount, ct);

        var credits = await _context.Transactions
            .Where(t => t.CreditAccountId == accountId && t.OccurredAt <= asOfDate)
            .SumAsync(t => t.Amount, ct);

        return debits - credits;
    }

    public async Task AddAsync(Transaction transaction, CancellationToken ct = default)
    {
        await _context.Transactions.AddAsync(transaction, ct);
    }
}
