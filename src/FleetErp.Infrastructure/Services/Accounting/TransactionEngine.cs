using FleetErp.Application.Accounting.Interfaces;
using FleetErp.Application.Audit.Interfaces;
using FleetErp.Application.Lookups.Interfaces;
using FleetErp.Domain.Entities.Accounting;
using FleetErp.Shared.Constants;

namespace FleetErp.Infrastructure.Services.Accounting;

/// <summary>
/// Transaction engine implementation.
/// All financial operations go through this engine to maintain the double-entry ledger.
/// </summary>
public class TransactionEngine : ITransactionEngine
{
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ILookupRepository _lookupRepository;
    private readonly IAuditLogger _auditLogger;

    // Well-known account codes for system accounts
    private const string CashboxAccountCode = "CASHBOX-MAIN";
    private const string CompanyRevenueAccountCode = "COMPANY-REVENUE";

    public TransactionEngine(
        IAccountRepository accountRepository,
        ITransactionRepository transactionRepository,
        ILookupRepository lookupRepository,
        IAuditLogger auditLogger)
    {
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
        _lookupRepository = lookupRepository;
        _auditLogger = auditLogger;
    }

    public async Task PostRentalCreatedAsync(int rentalId, int customerId, decimal amount, CancellationToken ct = default)
    {
        if (amount <= 0) return;

        // Get or create customer account
        var customerAccountId = await EnsureAccountExistsAsync(
            AccountOwnerTypes.Customer, customerId, $"Customer #{customerId}", ct);

        // Get company revenue account
        var revenueAccount = await GetSystemAccountAsync(CompanyRevenueAccountCode, ct);

        // Get transaction type
        var transactionType = await _lookupRepository.GetByTypeAndCodeAsync(
            LookupTypes.TransactionType, "RENTAL_CREATED", ct);

        // Create transaction: Debit Customer (they owe us), Credit Revenue
        var transaction = new Transaction
        {
            TransactionTypeId = transactionType?.Id ?? 0,
            DebitAccountId = customerAccountId,
            CreditAccountId = revenueAccount.Id,
            Amount = amount,
            ReferenceType = TransactionReferenceTypes.Rental,
            ReferenceId = rentalId,
            OccurredAt = DateTime.UtcNow,
            Notes = $"Rental #{rentalId} created"
        };

        await PostTransactionAsync(transaction, ct);

        _auditLogger.Log(AuditActions.InvoiceGenerated, nameof(Transaction), transaction.Id,
            userId: null, $"Rental created transaction posted: {amount:C}");
    }

    public async Task PostRentalPaymentReceivedAsync(int rentalPaymentId, int customerId, decimal amount, CancellationToken ct = default)
    {
        if (amount <= 0) return;

        // Get cashbox account
        var cashboxAccount = await GetSystemAccountAsync(CashboxAccountCode, ct);

        // Get customer account
        var customerAccountId = await EnsureAccountExistsAsync(
            AccountOwnerTypes.Customer, customerId, $"Customer #{customerId}", ct);

        // Get transaction type
        var transactionType = await _lookupRepository.GetByTypeAndCodeAsync(
            LookupTypes.TransactionType, "PAYMENT_RECEIVED", ct);

        // Create transaction: Debit Cashbox (cash in), Credit Customer (reduce what they owe)
        var transaction = new Transaction
        {
            TransactionTypeId = transactionType?.Id ?? 0,
            DebitAccountId = cashboxAccount.Id,
            CreditAccountId = customerAccountId,
            Amount = amount,
            ReferenceType = TransactionReferenceTypes.RentalPayment,
            ReferenceId = rentalPaymentId,
            OccurredAt = DateTime.UtcNow,
            Notes = $"Payment #{rentalPaymentId} received"
        };

        await PostTransactionAsync(transaction, ct);

        _auditLogger.Log(AuditActions.PaymentReceived, nameof(Transaction), transaction.Id,
            userId: null, $"Payment received transaction posted: {amount:C}");
    }

    public async Task PostRentalPaymentVoidedAsync(int rentalPaymentId, int customerId, decimal amount, CancellationToken ct = default)
    {
        if (amount <= 0) return;

        // Get cashbox account
        var cashboxAccount = await GetSystemAccountAsync(CashboxAccountCode, ct);

        // Get customer account
        var customerAccountId = await EnsureAccountExistsAsync(
            AccountOwnerTypes.Customer, customerId, $"Customer #{customerId}", ct);

        // Get transaction type
        var transactionType = await _lookupRepository.GetByTypeAndCodeAsync(
            LookupTypes.TransactionType, "PAYMENT_VOIDED", ct);

        // Reverse: Debit Customer (increase what they owe), Credit Cashbox (cash out)
        var transaction = new Transaction
        {
            TransactionTypeId = transactionType?.Id ?? 0,
            DebitAccountId = customerAccountId,
            CreditAccountId = cashboxAccount.Id,
            Amount = amount,
            ReferenceType = TransactionReferenceTypes.RentalPayment,
            ReferenceId = rentalPaymentId,
            OccurredAt = DateTime.UtcNow,
            Notes = $"Payment #{rentalPaymentId} voided"
        };

        await PostTransactionAsync(transaction, ct);

        _auditLogger.Log(AuditActions.PaymentVoided, nameof(Transaction), transaction.Id,
            userId: null, $"Payment voided transaction posted: {amount:C}");
    }

    public async Task PostMaintenanceExpenseAsync(int maintenanceId, int vehicleId, decimal amount, CancellationToken ct = default)
    {
        if (amount <= 0) return;

        // Get cashbox account
        var cashboxAccount = await GetSystemAccountAsync(CashboxAccountCode, ct);

        // Get or create vehicle operational account
        var vehicleAccountId = await EnsureAccountExistsAsync(
            AccountOwnerTypes.Vehicle, vehicleId, $"Vehicle #{vehicleId}", ct);

        // Get transaction type
        var transactionType = await _lookupRepository.GetByTypeAndCodeAsync(
            LookupTypes.TransactionType, "MAINTENANCE_EXPENSE", ct);

        // Create transaction: Debit Vehicle (expense), Credit Cashbox (cash out)
        var transaction = new Transaction
        {
            TransactionTypeId = transactionType?.Id ?? 0,
            DebitAccountId = vehicleAccountId,
            CreditAccountId = cashboxAccount.Id,
            Amount = amount,
            ReferenceType = TransactionReferenceTypes.MaintenanceExpense,
            ReferenceId = maintenanceId,
            OccurredAt = DateTime.UtcNow,
            Notes = $"Maintenance #{maintenanceId} expense"
        };

        await PostTransactionAsync(transaction, ct);

        _auditLogger.Log(AuditActions.Created, nameof(Transaction), transaction.Id,
            userId: null, $"Maintenance expense transaction posted: {amount:C}");
    }

    public async Task PostInvestorWithdrawalAsync(int withdrawalId, int investorId, decimal amount, CancellationToken ct = default)
    {
        if (amount <= 0) return;

        // Get cashbox account
        var cashboxAccount = await GetSystemAccountAsync(CashboxAccountCode, ct);

        // Get investor account
        var investorAccountId = await EnsureAccountExistsAsync(
            AccountOwnerTypes.Investor, investorId, $"Investor #{investorId}", ct);

        // Get transaction type
        var transactionType = await _lookupRepository.GetByTypeAndCodeAsync(
            LookupTypes.TransactionType, "INVESTOR_WITHDRAWAL", ct);

        // Create transaction: Debit Investor (reduce payable), Credit Cashbox (cash out)
        var transaction = new Transaction
        {
            TransactionTypeId = transactionType?.Id ?? 0,
            DebitAccountId = investorAccountId,
            CreditAccountId = cashboxAccount.Id,
            Amount = amount,
            ReferenceType = TransactionReferenceTypes.InvestorWithdrawal,
            ReferenceId = withdrawalId,
            OccurredAt = DateTime.UtcNow,
            Notes = $"Investor #{investorId} withdrawal"
        };

        await PostTransactionAsync(transaction, ct);

        _auditLogger.Log(AuditActions.WithdrawalProcessed, nameof(Transaction), transaction.Id,
            userId: null, $"Investor withdrawal transaction posted: {amount:C}");
    }

    public async Task PostBankTransferAsync(int fromAccountId, int toAccountId, decimal amount, string? notes, CancellationToken ct = default)
    {
        if (amount <= 0) return;

        // Get transaction type
        var transactionType = await _lookupRepository.GetByTypeAndCodeAsync(
            LookupTypes.TransactionType, "BANK_TRANSFER", ct);

        // Create transaction: Debit To Account, Credit From Account
        var transaction = new Transaction
        {
            TransactionTypeId = transactionType?.Id ?? 0,
            DebitAccountId = toAccountId,
            CreditAccountId = fromAccountId,
            Amount = amount,
            ReferenceType = TransactionReferenceTypes.BankTransfer,
            ReferenceId = null,
            OccurredAt = DateTime.UtcNow,
            Notes = notes ?? "Bank transfer"
        };

        await PostTransactionAsync(transaction, ct);

        _auditLogger.Log(AuditActions.Created, nameof(Transaction), transaction.Id,
            userId: null, $"Bank transfer transaction posted: {amount:C}");
    }

    public async Task<int> EnsureAccountExistsAsync(string ownerType, int ownerId, string name, CancellationToken ct = default)
    {
        // Check if account already exists
        var existingAccount = await _accountRepository.GetByOwnerAsync(ownerType, ownerId, ct);
        if (existingAccount != null)
        {
            return existingAccount.Id;
        }

        // Get account type lookup
        var accountType = await _lookupRepository.GetByTypeAndCodeAsync(
            LookupTypes.AccountType, ownerType.ToUpperInvariant(), ct);

        // Create new account
        var account = new Account
        {
            AccountTypeId = accountType?.Id ?? 0,
            OwnerType = ownerType,
            OwnerId = ownerId,
            Code = $"{ownerType.ToUpperInvariant()}-{ownerId:D6}",
            Name = name,
            Currency = "USD",
            IsActive = true
        };

        await _accountRepository.AddAsync(account, ct);

        return account.Id;
    }

    private async Task<Account> GetSystemAccountAsync(string code, CancellationToken ct)
    {
        var account = await _accountRepository.GetByCodeAsync(code, ct);
        if (account == null)
        {
            throw new InvalidOperationException($"System account '{code}' not found. Please run database seeding.");
        }
        return account;
    }

    private async Task PostTransactionAsync(Transaction transaction, CancellationToken ct)
    {
        // Get both accounts
        var debitAccount = await _accountRepository.GetByIdAsync(transaction.DebitAccountId, ct);
        var creditAccount = await _accountRepository.GetByIdAsync(transaction.CreditAccountId, ct);

        if (debitAccount == null || creditAccount == null)
        {
            throw new InvalidOperationException("One or both accounts not found for transaction.");
        }

        // Update balances using internal methods
        debitAccount.Debit(transaction.Amount);
        creditAccount.Credit(transaction.Amount);

        // Save transaction and updated accounts
        await _transactionRepository.AddAsync(transaction, ct);
        _accountRepository.Update(debitAccount);
        _accountRepository.Update(creditAccount);
    }
}
