namespace FleetErp.Application.Accounting.Interfaces;

/// <summary>
/// Transaction engine interface for all financial operations.
/// NEVER update Account.Balance directly — always go through this engine.
/// Balances are derived from Transactions.
/// </summary>
public interface ITransactionEngine
{
    /// <summary>
    /// Post a transaction when a rental is created.
    /// Debits Customer receivable (they owe us), Credits Company revenue.
    /// </summary>
    Task PostRentalCreatedAsync(int rentalId, int customerId, decimal amount, CancellationToken ct = default);

    /// <summary>
    /// Post a transaction when a rental payment is received.
    /// Debits Cashbox (cash coming in), Credits Customer receivable (reducing what they owe).
    /// </summary>
    Task PostRentalPaymentReceivedAsync(int rentalPaymentId, int customerId, decimal amount, CancellationToken ct = default);

    /// <summary>
    /// Post a transaction when a rental payment is voided/deleted.
    /// Reverses the original payment transaction.
    /// Debits Customer receivable, Credits Cashbox.
    /// </summary>
    Task PostRentalPaymentVoidedAsync(int rentalPaymentId, int customerId, decimal amount, CancellationToken ct = default);

    /// <summary>
    /// Post a transaction for a maintenance expense.
    /// Debits Vehicle operational account (expense), Credits Cashbox.
    /// </summary>
    Task PostMaintenanceExpenseAsync(int maintenanceId, int vehicleId, decimal amount, CancellationToken ct = default);

    /// <summary>
    /// Post a transaction for an investor withdrawal.
    /// Debits Investor payable (reducing what we owe them), Credits Cashbox.
    /// </summary>
    Task PostInvestorWithdrawalAsync(int withdrawalId, int investorId, decimal amount, CancellationToken ct = default);

    /// <summary>
    /// Post a bank transfer between accounts.
    /// </summary>
    Task PostBankTransferAsync(int fromAccountId, int toAccountId, decimal amount, string? notes, CancellationToken ct = default);

    /// <summary>
    /// Ensure an account exists for an entity. Creates one if it doesn't exist.
    /// Returns the account ID.
    /// </summary>
    Task<int> EnsureAccountExistsAsync(string ownerType, int ownerId, string name, CancellationToken ct = default);
}
