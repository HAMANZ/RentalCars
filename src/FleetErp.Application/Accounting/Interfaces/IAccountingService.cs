namespace FleetErp.Application.Accounting.Interfaces;

/// <summary>
/// Accounting service for querying account balances and transactions.
/// Balances are always calculated from Transactions, never stored directly.
/// </summary>
public interface IAccountingService
{
    /// <summary>
    /// Get the current balance for an account (calculated from transactions).
    /// </summary>
    Task<decimal> GetAccountBalanceAsync(int accountId, CancellationToken ct = default);

    /// <summary>
    /// Get the balance for an account as of a specific date.
    /// </summary>
    Task<decimal> GetAccountBalanceAsOfAsync(int accountId, DateTime asOfDate, CancellationToken ct = default);
}
