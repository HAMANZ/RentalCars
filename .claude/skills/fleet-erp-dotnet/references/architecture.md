# Architecture — Clean Architecture / Modular Monolith

## Guiding constraints (from project philosophy)

SOLID, DRY, KISS, YAGNI. Prefer explicit code over clever/generic code. Every
feature must solve a stated business problem — don't build for imagined future
requirements except where "Future Features" in `domain-model.md` explicitly says to
leave room.

## Solution / folder structure

```
FleetErp.sln
src/
  FleetErp.Domain/
    Entities/
      Investors/          Vehicles/          Drivers/
      Contracts/           Invoices/          Payments/
      Accounting/          Maintenance/       Insurance/
      Security/            Lookups/           Audit/
    Enums/                 (only for truly fixed, never-changing values — see note)
    Common/                (BaseEntity, IAuditable, ISoftDelete interfaces)

  FleetErp.Application/
    Investors/  Vehicles/  Drivers/  Contracts/  Invoices/  Payments/
    Accounting/  Maintenance/  Insurance/  Security/  Lookups/  Audit/
      <Module>/
        Dtos/
        Interfaces/         (I<Module>Service, I<Module>Repository)
        Services/
        Validators/          (FluentValidation)
    Common/
      IUnitOfWork.cs
      Result.cs / PagedResult.cs
      Exceptions/

  FleetErp.Infrastructure/
    Persistence/
      FleetErpDbContext.cs
      Configurations/        (IEntityTypeConfiguration<T> per entity, fluent API)
      Migrations/
      Repositories/           (generic base + per-aggregate overrides)
      UnitOfWork.cs
    Identity/                 (JWT issuing, password hashing)
    BackgroundJobs/            (notification scanner, expiration checks)

  FleetErp.Api/
    Controllers/                (API controllers)
    Views/                     (existing Razor dashboard views — extend, don't redesign)
    wwwroot/                    (existing static assets — CSS/JS/images for the dashboard)
    Middleware/                (global exception handler, audit interceptor)
    Auth/                      (permission-based [Authorize] policies)
    Program.cs                 (DI composition root)

  FleetErp.Shared/
    Constants/
    Extensions/

tests/
  FleetErp.UnitTests/
  FleetErp.IntegrationTests/
```

**Module-per-folder, not module-per-project.** This is a modular monolith: each
business module gets a consistent folder under Domain/Application/Infrastructure,
but everything compiles into the same few assemblies. Don't spin up a new .csproj
per module — that's microservices-style overhead this project explicitly doesn't
want yet (see Future Features in domain-model.md).

## Layer responsibilities

- **Domain**: Entities, invariants, domain exceptions. No EF Core references, no
  framework dependencies. An entity should be able to reject an invalid state
  transition itself (e.g. `Invoice.MarkPaid()` throws if already `Cancelled`).
- **Application**: Use-case orchestration. Services depend on repository/UoW
  *interfaces* only (defined here, implemented in Infrastructure). DTOs are the only
  things that cross into the Api layer — never leak Domain entities out.
- **Infrastructure**: EF Core, MySQL, repository implementations, external
  integrations (SMS/email when built later), background jobs.
- **Api**: Controllers are thin — validate the DTO, call one service method, return
  the result. No business logic in controllers, ever. This project also hosts the
  server-rendered **Razor/MVC dashboard** (`Views/` + `wwwroot/`) — extend the
  existing views, don't redesign or swap in a SPA. API controllers and MVC
  controllers coexist here; keep API routes under `api/` and view controllers
  separate.

## Patterns

- **Repository + Unit of Work**: one repository per aggregate root (e.g.
  `IInvestorRepository`, not one per table). `IUnitOfWork.SaveChangesAsync()` wraps
  each service-layer operation in a single transaction.
- **Service Layer**: business rules live here. This is also where the
  Accounting/Transaction engine gets invoked (see `accounting-engine.md`) — a
  service like `PaymentService.RecordPaymentAsync()` both updates domain state and
  triggers the transaction engine within the same unit of work.
- **CQRS**: skip it by default. Only introduce MediatR/CQRS for a module if reads
  and writes have genuinely diverging complexity (e.g. a reporting module with heavy
  denormalized read models) — and even then, prefer a simple dedicated
  "query service" over a full CQRS/mediator pipeline unless the user asks for one.
- **Dependency Injection**: constructor injection everywhere; composition root is
  `Program.cs` in the Api project. Register services/repositories per-module using
  extension methods (`services.AddInvestorModule()`, etc.) to keep `Program.cs`
  readable as modules grow.

## Dynamic Lookup Architecture

Any concept listed under "Dynamic system" in `domain-model.md` is a database-driven
lookup, not a C# enum. Standard shape:

```csharp
public class LookupItem : BaseEntity
{
    public string LookupType { get; set; }   // e.g. "InvoiceStatus", "FuelType"
    public string Code { get; set; }
    public string Name { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
}
```

Either one shared `LookupItem` table keyed by `LookupType` (simplest, fewer tables,
good default for the MVP) or dedicated tables per lookup type if a lookup needs
extra fields (e.g. `InsuranceCompany` needing a contact/phone). Default to the
shared table unless a specific lookup clearly needs extra columns — don't create 20
near-identical lookup tables by default (YAGNI).

Entities reference lookups via a FK `int` (e.g. `Invoice.StatusId`), never a
free-text/varchar status column.

## RBAC / Permissions

- `Role`, `Permission`, `RolePermission`, `UserRole` tables — all DB-driven.
- Enforce via a custom `[Authorize(Policy = "Invoices.Approve")]`-style policy that
  checks the current user's permissions loaded at login/claims time — don't
  hardcode role name checks like `[Authorize(Roles = "Accountant")]` in code, since
  roles/permissions must be addable without a deploy.

## Audit Logging

Prefer explicit calls from Service-layer methods (`_auditLogger.Log(action, entity,
userId, details)`) for anything financial or contractual, so entries are
business-meaningful. Use an EF Core `SaveChanges` interceptor only as a generic
safety net for entities that don't already get an explicit audit call.

## Notifications

Implement as a background job (`IHostedService` or a scheduled worker) that scans
for upcoming expirations/due items on a cadence (e.g. daily) and writes
`Notification` rows a user reads from the Dashboard — don't compute notifications
live on every request.

## Naming conventions

- Entities: singular PascalCase (`Investor`, `Vehicle`, `Contract`).
- DTOs: `<Entity>Dto`, `Create<Entity>Request`, `Update<Entity>Request`.
- Services: `I<Entity>Service` / `<Entity>Service`.
- Repositories: `I<AggregateRoot>Repository` / `<AggregateRoot>Repository`.
- Migrations: `<Timestamp>_<PhaseOrFeature>_<Description>`.
