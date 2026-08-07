# Code Templates

Use these skeletons verbatim as the canonical shape for this project. When you
generate a new module, copy the relevant template and fill it in — do NOT invent a
different structure each time. Consistency across modules is the whole point.

## Conventions (pin these — don't improvise per module)

- **Async everywhere** for I/O. Suffix async methods with `Async`. Take a
  `CancellationToken` on repository/service methods.
- **Result pattern for expected failures, exceptions for bugs.** Services return
  `Result<T>` for outcomes the caller must handle (not found, validation, business
  rule violated). Reserve thrown exceptions for truly exceptional/programmer errors,
  caught by a global exception-handling middleware.
- **Controllers stay thin**: validate DTO → call one service method → translate
  `Result<T>` to an `IActionResult`. No business logic, no `DbContext`, no repos in
  controllers.
- **Never expose domain entities over the API** — map to DTOs.
- **Every mutating service method**: runs inside the Unit of Work, writes an Audit
  Log entry, and (if financial) calls the transaction engine — all in one commit.
- **API envelope & status codes**: `200/201` with body on success, `400` for
  validation, `404` for not found, `409` for business-rule conflicts, `401/403` for
  auth. Use the `Result<T>` → `IActionResult` mapper below so this is uniform.
- **Pagination**: list endpoints take `page`, `pageSize` and return `PagedResult<T>`.

## Common building blocks (Domain/Common + Application/Common)

```csharp
// FleetErp.Domain/Common/BaseEntity.cs
public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }        // soft delete; never hard-delete financial/contractual rows
}

// FleetErp.Application/Common/Result.cs
public class Result
{
    public bool IsSuccess { get; }
    public string? Error { get; }
    public ResultErrorType ErrorType { get; }
    protected Result(bool ok, string? error, ResultErrorType type)
        => (IsSuccess, Error, ErrorType) = (ok, error, type);

    public static Result Success() => new(true, null, ResultErrorType.None);
    public static Result NotFound(string e) => new(false, e, ResultErrorType.NotFound);
    public static Result Invalid(string e) => new(false, e, ResultErrorType.Validation);
    public static Result Conflict(string e) => new(false, e, ResultErrorType.Conflict);
}

public class Result<T> : Result
{
    public T? Value { get; }
    private Result(T value) : base(true, null, ResultErrorType.None) => Value = value;
    private Result(string e, ResultErrorType t) : base(false, e, t) { }
    public static Result<T> Success(T value) => new(value);
    public static new Result<T> NotFound(string e) => new(e, ResultErrorType.NotFound);
    public static new Result<T> Invalid(string e) => new(e, ResultErrorType.Validation);
    public static new Result<T> Conflict(string e) => new(e, ResultErrorType.Conflict);
}

public enum ResultErrorType { None, NotFound, Validation, Conflict }

// FleetErp.Application/Common/PagedResult.cs
public class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; init; } = [];
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}
```

## Unit of Work + generic repository (Application interfaces, Infrastructure impl)

```csharp
// FleetErp.Application/Common/IUnitOfWork.cs
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}

// FleetErp.Application/<Module>/Interfaces/I<AggregateRoot>Repository.cs
public interface IInvestorRepository
{
    Task<Investor?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<PagedResult<Investor>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default);
    Task AddAsync(Investor investor, CancellationToken ct = default);
    void Update(Investor investor);
    // No hard Delete for financial/contractual aggregates — soft-delete via entity flag.
}
```

## Domain entity with enforced invariant

```csharp
// FleetErp.Domain/Entities/Invoices/Invoice.cs
public class Invoice : BaseEntity
{
    public int ContractId { get; private set; }
    public int StatusId { get; private set; }        // FK to InvoiceStatus lookup — NOT an enum
    public decimal TotalAmount { get; private set; }
    public decimal Discount { get; private set; }
    public decimal AmountPaid { get; private set; }

    public decimal Balance => TotalAmount - Discount - AmountPaid;

    // Status is recalculated, never set arbitrarily. Called by the payment flow.
    public void RecalculateStatus(int paidStatusId, int partialStatusId, int pendingStatusId, int cancelledStatusId)
    {
        if (StatusId == cancelledStatusId)
            throw new DomainException("Cannot change status of a cancelled invoice.");
        StatusId = Balance <= 0 ? paidStatusId
                 : AmountPaid > 0 ? partialStatusId
                 : pendingStatusId;
    }
}
```

## EF Core configuration (fluent API, one per entity)

```csharp
// FleetErp.Infrastructure/Persistence/Configurations/InvoiceConfiguration.cs
public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> b)
    {
        b.ToTable("invoices");
        b.HasKey(x => x.Id);
        b.Property(x => x.TotalAmount).HasPrecision(18, 2);
        b.Property(x => x.Discount).HasPrecision(18, 2);
        b.Property(x => x.AmountPaid).HasPrecision(18, 2);
        b.HasIndex(x => x.ContractId);
        b.HasQueryFilter(x => !x.IsDeleted);   // global soft-delete filter
    }
}
```

## Canonical service method (UoW + audit + transaction engine in one commit)

```csharp
// FleetErp.Application/Payments/Services/PaymentService.cs
public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _payments;
    private readonly IInvoiceRepository _invoices;
    private readonly ITransactionEngine _accounting;
    private readonly IAuditLogger _audit;
    private readonly IUnitOfWork _uow;
    private readonly ICurrentUser _user;

    public PaymentService(/* ctor injection of all the above */) { /* ... */ }

    public async Task<Result<PaymentDto>> RecordPaymentAsync(CreatePaymentRequest req, CancellationToken ct)
    {
        var invoice = await _invoices.GetByIdAsync(req.InvoiceId, ct);
        if (invoice is null) return Result<PaymentDto>.NotFound("Invoice not found.");
        if (req.Amount <= 0) return Result<PaymentDto>.Invalid("Amount must be positive.");

        var payment = new Payment(req.InvoiceId, req.Amount, req.PaymentMethodId);
        await _payments.AddAsync(payment, ct);

        invoice.ApplyPayment(req.Amount);                      // updates AmountPaid + recalculates status
        _invoices.Update(invoice);

        // Financial side-effect goes through the engine — never touch Account.Balance directly.
        await _accounting.PostPaymentReceivedAsync(payment, ct);

        _audit.Log("PaymentReceived", nameof(Payment), payment.Id, _user.Id,
                   $"Applied {req.Amount:C} to invoice {invoice.Id}");

        await _uow.SaveChangesAsync(ct);                        // one atomic commit for all of the above
        return Result<PaymentDto>.Success(payment.ToDto());
    }
}
```

## Thin controller + Result→IActionResult mapper

```csharp
// FleetErp.Api/Controllers/PaymentsController.cs
[ApiController]
[Route("api/payments")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _service;
    public PaymentsController(IPaymentService service) => _service = service;

    [HttpPost]
    [Authorize(Policy = "Payments.Create")]     // DB-driven permission, not a role-name check
    public async Task<IActionResult> Record([FromBody] CreatePaymentRequest req, CancellationToken ct)
        => (await _service.RecordPaymentAsync(req, ct)).ToActionResult(this);
}

// FleetErp.Api/Common/ResultExtensions.cs
public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> r, ControllerBase c) => r switch
    {
        { IsSuccess: true }                          => c.Ok(r.Value),
        { ErrorType: ResultErrorType.NotFound }      => c.NotFound(r.Error),
        { ErrorType: ResultErrorType.Conflict }      => c.Conflict(r.Error),
        _                                            => c.BadRequest(r.Error),
    };
}
```

## Validator (FluentValidation)

```csharp
// FleetErp.Application/Payments/Validators/CreatePaymentRequestValidator.cs
public class CreatePaymentRequestValidator : AbstractValidator<CreatePaymentRequest>
{
    public CreatePaymentRequestValidator()
    {
        RuleFor(x => x.InvoiceId).GreaterThan(0);
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.PaymentMethodId).GreaterThan(0);
    }
}
```

## Per-module DI registration (keeps Program.cs readable)

```csharp
// FleetErp.Application/Payments/PaymentModule.cs
public static class PaymentModule
{
    public static IServiceCollection AddPaymentModule(this IServiceCollection s)
    {
        s.AddScoped<IPaymentService, PaymentService>();
        s.AddScoped<IPaymentRepository, PaymentRepository>();
        s.AddValidatorsFromAssemblyContaining<CreatePaymentRequestValidator>();
        return s;
    }
}
// Program.cs then reads: builder.Services.AddPaymentModule();
```

## Definition of done for any module

A module isn't finished until it has: domain entity(+invariants), EF configuration,
migration, repository (interface+impl), service (with UoW+audit+transaction-engine
wiring where relevant), DTOs+validators, thin controller with the correct DB-driven
`[Authorize]` policy, and audit logging on every mutation. Missing any of these =
not done.
