using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.DomainLayer.Models
{

    /// <summary>
    /// Represents a financial transaction in the double-entry ledger.
    /// Each transaction has a debit account and a credit account.
    /// The sum of all debits equals the sum of all credits (accounting equation).
    /// </summary>
    public class STransaction : BaseEntity
{
        [Key]
    public long TransactionId { get; set; }
    public long TransactionTypeId { get; set; }
    public string Description { get; set; }
    public int BranchIdId { get; set; }

    [ForeignKey("TransactionTypeId")]
    public STransactionType STransactionType { get; set; }

    [ForeignKey("BranchId")]
    public Branch Branch { get; set; }
        /// <summary>
        /// The account being debited (receiving value or recording receivable).
        /// </summary>
        public long DebitAccountId { get; set; }

    /// <summary>
    /// The account being credited (giving value or recording payable).
    /// </summary>
    public long CreditAccountId { get; set; }

    /// <summary>
    /// The amount of the transaction. Always positive.
    /// </summary>
    public double Amount { get; set; }

    /// <summary>
    /// The type of entity this transaction references.
    /// Values: "Rental", "RentalPayment", "MaintenanceExpense", "InvestorWithdrawal", "BankTransfer"
    /// </summary>
    

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

    public ICollection<STransactionDocuments> Documents { get; set; }

        // Navigation properties
    public STransactionType TransactionType { get; set; } = null!;
    public SAccount DebitAccount { get; set; } = null!;
    public SAccount CreditAccount { get; set; } = null!;
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
}
