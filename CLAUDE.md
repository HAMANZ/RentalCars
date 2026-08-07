# Fleet Management & Taxi Rental ERP

Always-on rules for this project. For module checklists, C# templates, the full
domain model, the accounting engine, the phase plan, and concrete DB schema, consult
the **`fleet-erp-dotnet` skill** (in `.claude/skills/fleet-erp-dotnet/`) — read its
SKILL.md and the relevant file under `references/` before writing code for any
module. This file holds only what must be true in every session.

## What this system is

A Clean Architecture, Modular Monolith ERP (ASP.NET Core + EF Core + MySQL) for
companies that manage investor-owned taxi fleets rented to drivers. The company
**never owns vehicles** — it manages them for investors and automates the lifecycle:
investor onboarding → vehicle assignment → driver contracts → invoicing → payments →
accounting → reporting.

## Tech stack (fixed — never substitute)

- Backend: ASP.NET Core (.NET, latest LTS)
- ORM: Entity Framework Core (fluent config, not data annotations)
- DB: MySQL (snake_case tables, DECIMAL(18,2) money, utf8mb4)
- Auth: JWT + database-driven Role-Based Access Control
- Frontend: server-rendered ASP.NET Core MVC / Razor views (no SPA/JS framework).
  Extend the existing Dashboard views in the presentation project — never redesign.
- Architecture: Clean Architecture, Modular Monolith (module-per-folder, NOT
  module-per-project)
- Patterns: Repository, Unit of Work, Service Layer, DI. CQRS only when a module
  genuinely justifies it — default to plain service methods.

## Non-negotiable invariants

1. **Never mutate an Account balance directly.** Every financial action goes through
   the transaction engine (`ITransactionEngine` / `IAccountingService`), inside the
   same Unit of Work as the business action. Balances are derived from Transactions.
2. **Never hard-delete contracts, invoices, or financial rows.** Soft-delete
   (`is_deleted`) or status transitions (Cancelled/Terminated/Voided) only.
3. **Nothing dynamic is hardcoded.** Statuses, payment methods, expense/maintenance/
   vehicle/fuel/transmission/document/notification types, roles, permissions, menus
   come from lookup tables. Status/type columns are `*_id INT FK` to a lookup, never
   a C# enum or free-text varchar.
4. **Every mutation writes an Audit Log entry** and, if financial, posts a
   Transaction — all in one atomic commit.
5. **Controllers stay thin**: validate DTO → call one service method → map
   `Result<T>` to `IActionResult`. No business logic, no DbContext, no repos in
   controllers. Never expose domain entities over the API — use DTOs.

## Code conventions

- Async everywhere for I/O; suffix `Async`; take a `CancellationToken`.
- `Result<T>` for expected failures (not found / validation / conflict); throw only
  for real bugs, caught by global exception middleware.
- One repository per aggregate root, not per table.
- Per-module DI via extension methods (`services.AddInvestorModule()`), registered
  in `Program.cs`.
- `[Authorize(Policy = "Module.Action")]` using DB-driven permissions — never
  `[Authorize(Roles = "...")]` hardcoded.
- Money is `DECIMAL(18,2)`; set precision in EF fluent config. EF global query
  filter for soft delete.

## Definition of done for a module

Domain entity (with invariants) · EF configuration · migration · repository
(interface + impl) · service (UoW + audit + transaction-engine wiring where relevant)
· DTOs + FluentValidation validators · thin controller with correct DB-driven
`[Authorize]` policy · audit logging on every mutation. Missing any = not done.

## Build order

Follow the phased plan in the skill's `references/phases.md` (Phase 0 foundation →
1 auth/RBAC → 2 config/lookups → 3 investors → 4 vehicles → 5 drivers → 6 contracts →
7 invoices → 8 payments → 9 accounting → 10 maintenance → 11 insurance → 12 reports/
dashboard/notifications). Don't build a phase whose dependencies don't exist yet;
flag it if asked to, but help if the user insists.

## Philosophy

KISS, YAGNI, DRY, SOLID. Build the simplest production-ready version of what's asked.
No features/entities/abstractions that weren't requested or implied. Explicit code
over clever code. When a request is genuinely new/ambiguous, ask one clarifying
question rather than guessing.
