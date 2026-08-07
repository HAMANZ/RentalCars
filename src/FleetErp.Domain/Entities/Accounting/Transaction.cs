using FleetErp.Domain.Common;
using FleetErp.Domain.Entities.Lookups;

namespace FleetErp.Domain.Entities.Accounting;

/// <summary>
/// Represents a financial transaction in the double-entry ledger.
/// Each transaction has a debit account and a credit account.
/// The sum of all debits equals the sum of all credits (accounting equation).
/// </summary>
public class Transaction : BaseEntity
{
    public int TransactionTypeId { get; set; }

    /// <summary>
    /// The account being debited (receiving value or recording receivable).
    /// </summary>
    public int DebitAccountId { get; set; }

    /// <summary>
    /// The account being credited (giving value or recording payable).
    /// </summary>
    public int CreditAccountId { get; set; }

    /// <summary>
    /// The amount of the transaction. Always positive.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// The type of entity this transaction references.
    /// Values: "Rental", "RentalPayment", "MaintenanceExpense", "InvestorWithdrawal", "BankTransfer"
    /// </summary>
    public string? ReferenceType { get; set; }

    /// <summary>
    /// The ID of the referenced entity (e.g., RentalPayment.Id).
    /// </summary>
    public int? ReferenceId { get; set; }

    /// <summary>
    /// When the transaction occurred (business date).
    /// </summary>
    public DateTime OccurredAt { get; set; }

    /// <summary>
    /// Optional description or notes about the transaction.
    /// </summary>
    public string? Notes { get; set; }

    // Navigation properties
    public LookupItem TransactionType { get; set; } = null!;
    public Account DebitAccount { get; set; } = null!;
    public Account CreditAccount { get; set; } = null!;
}

/// <summary>
/// Well-known reference types for transactions.
/// </summary>
public static class TransactionReferenceTypes
{
    public const string Rental = "Rental";
    public const string RentalPayment = "RentalPayment";
    public const string MaintenanceExpense = "MaintenanceExpense";
    public const string InvestorWithdrawal = "InvestorWithdrawal";
    public const string BankTransfer = "BankTransfer";
}
