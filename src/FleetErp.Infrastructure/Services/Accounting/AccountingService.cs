using FleetErp.Application.Accounting.Interfaces;

namespace FleetErp.Infrastructure.Services.Accounting;

/// <summary>
/// Accounting service for querying account balances and transactions.
/// </summary>
public class AccountingService : IAccountingService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;

    public AccountingService(
        ITransactionRepository transactionRepository,
        IAccountRepository accountRepository)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
    }

    public async Task<decimal> GetAccountBalanceAsync(int accountId, CancellationToken ct = default)
    {
        // For performance, we use the maintained balance on the account
        // This is kept in sync by the TransactionEngine
        var account = await _accountRepository.GetByIdAsync(accountId, ct);
        return account?.Balance ?? 0;
    }

    public async Task<decimal> GetAccountBalanceAsOfAsync(int accountId, DateTime asOfDate, CancellationToken ct = default)
    {
        // For historical balance, we calculate from transactions
        return await _transactionRepository.GetAccountBalanceAsOfAsync(accountId, asOfDate, ct);
    }
}
