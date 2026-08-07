# Accounting Engine

Accounting is the heart of the system. **Never update an Account balance directly.**
Every financial action must go through a Transaction; balances are always derived
(sum of transactions, or a maintained running balance that is *only* ever updated as
a side effect of inserting a Transaction row within the same DB transaction/unit of
work).

## Accounts

Every financial entity owns an Account. Known account types:

- Investor Account
- Driver Account
- Company Account
- Cashbox Account
- Bank Account
- Vehicle Operational Account
- Supplier Account
- Workshop Account
- Expense Account
- Insurance Account

New account types must be addable from the database (a lookup/type table), not by
adding a new C# class per account type.

Suggested shape:

```csharp
public class Account : BaseEntity
{
    public int AccountTypeId { get; set; }     // FK to lookup, not enum
    public string OwnerType { get; set; }        // "Investor" | "Driver" | "Vehicle" | "Company" | ...
    public int? OwnerId { get; set; }             // FK to the owning entity, nullable for Company/Cashbox/Bank
    public decimal Balance { get; set; }           // maintained, but only mutated by the Transaction engine
    public string Currency { get; set; }
}

public class Transaction : BaseEntity
{
    public int TransactionTypeId { get; set; }    // FK to lookup: Invoice, Payment, Expense, Withdrawal, Transfer...
    public int DebitAccountId { get; set; }
    public int CreditAccountId { get; set; }
    public decimal Amount { get; set; }
    public string ReferenceType { get; set; }      // "Invoice" | "Payment" | "MaintenanceExpense" | ...
    public int? ReferenceId { get; set; }
    public DateTime OccurredAt { get; set; }
    public string Notes { get; set; }
}
```

A single `Transaction` row models a debit/credit pair, which keeps the ledger
double-entry-clean without needing a separate "TransactionLine" table for this
MVP's needs. If a future scenario needs more than two legs in one atomic move,
introduce a `TransactionLine` child table then — don't build it preemptively.

## Canonical transaction flows

### Invoice Created
```
Invoice Created → Driver Account (debit, owes money) → Company Revenue (credit) → Audit Log
```

### Payment Received
```
Payment Received → Cashbox (debit) → Driver Account (credit, reduces what they owe)
                 → Invoice (status recalculated from remaining balance) → Audit Log
```

### Maintenance Expense
```
Maintenance Expense → Vehicle Account (debit) → Cashbox (credit)
                    → Investor Account (debit, if the expense is billed to the investor)
                    → Audit Log
```

### Investor Withdrawal
```
Investor Withdrawal → Investor Account (debit) → Cashbox (credit) → Audit Log
```

### Bank Transfer
```
Bank Transfer → Cashbox (debit or credit depending on direction) → Bank (the other side) → Audit Log
```

## Implementation guidance

- Put transaction-creation logic in a dedicated `ITransactionEngine` /
  `IAccountingService` in the Application layer, called by feature services
  (`PaymentService`, `InvoiceService`, `MaintenanceService`, etc.) — don't
  reimplement the debit/credit logic separately in each module's service.
- Every call into the transaction engine must run inside the same
  `IUnitOfWork`/DB transaction as the business action that triggered it (e.g.
  recording a payment and posting its transaction must commit or roll back
  together).
- Invoice status is **recalculated**, not manually set, whenever a payment posts:
  compare `SUM(payments applied)` to `Invoice.TotalAmount` to derive
  Pending/Partially Paid/Paid.
- Every transaction-engine call ends with an Audit Log entry — treat this as
  non-optional plumbing inside the engine itself, not something each caller has to
  remember.
- Ledger/report queries (Investor Ledger, Cashbox Report, etc.) should read from the
  `Transaction` table filtered by account, not from denormalized running totals
  scattered across modules — the Transaction table is the single source of truth.
