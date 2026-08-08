using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.DomainLayer.Models {

/// <summary>
/// Represents a financial account in the system.
/// Every financial entity (Investor, Customer, Vehicle, Company, Cashbox, Bank) owns an Account.
/// IMPORTANT: Balance is maintained but ONLY mutated by the Transaction engine.
/// </summary>
public class SAccount : BaseEntity
{
        [Key]
        public long AccountId { get; set; }
        public int AccountTypeId { get; set; }

    /// <summary>
    /// The type of entity that owns this account.
    /// Values: "Investor", "Customer", "Vehicle", "Company", "Cashbox", "Bank"
    /// </summary>
    public string OwnerType { get; set; } = string.Empty;

    /// <summary>
    /// The ID of the owning entity. Null for singleton accounts (Company, Cashbox, Bank).
    /// </summary>
    public int? OwnerId { get; set; }

    /// <summary>
    /// Unique code for easy reference (e.g., "CASHBOX-MAIN", "INVESTOR-001").
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Display name for the account.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Current balance. NEVER update directly - only via TransactionEngine.
    /// Positive = credit balance (money owed TO this account).
    /// Negative = debit balance (money owed BY this account).
    /// </summary>
    public double Balance { get; private set; }

    public string Currency { get; set; } = "USD";

    public bool IsActive { get; set; } = true;

    // Navigation properties
    public SAccountType AccountType { get; set; } = null!;
        public ICollection<STransaction> DebitTransactions { get; set; }
            = new List<STransaction>();

        public ICollection<STransaction> CreditTransactions { get; set; }
            = new List<STransaction>();

        /// <summary>
        /// Apply a debit to this account (increases balance).
        /// WARNING: ONLY call from TransactionEngine - never call directly from services.
        /// </summary>
        public void Debit(double amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Debit amount must be positive", nameof(amount));
        Balance += amount;
    }

    /// <summary>
    /// Apply a credit to this account (decreases balance).
    /// WARNING: ONLY call from TransactionEngine - never call directly from services.
    /// </summary>
    public void Credit(double amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Credit amount must be positive", nameof(amount));
        Balance -= amount;
    }
}

/// <summary>
/// Well-known owner types for accounts.
/// </summary>
public static class AccountOwnerTypes
{
    public const string Investor = "Investor";
    public const string Customer = "Customer";
    public const string Vehicle = "Vehicle";
    public const string Company = "Company";
    public const string Cashbox = "Cashbox";
    public const string Bank = "Bank";
}
}