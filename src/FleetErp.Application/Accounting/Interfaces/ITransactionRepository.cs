using FleetErp.Application.Common;
using FleetErp.Domain.Entities.Accounting;

namespace FleetErp.Application.Accounting.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<Transaction>> GetByAccountIdAsync(int accountId, CancellationToken ct = default);
    Task<IReadOnlyList<Transaction>> GetByReferenceAsync(string referenceType, int referenceId, CancellationToken ct = default);
    Task<PagedResult<Transaction>> GetPagedByAccountAsync(int accountId, int page, int pageSize, DateTime? fromDate, DateTime? toDate, CancellationToken ct = default);
    Task<decimal> GetAccountBalanceAsync(int accountId, CancellationToken ct = default);
    Task<decimal> GetAccountBalanceAsOfAsync(int accountId, DateTime asOfDate, CancellationToken ct = default);
    Task AddAsync(Transaction transaction, CancellationToken ct = default);
}
