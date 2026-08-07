---
name: fleet-erp-dotnet
description: Build and extend the Fleet Management & Taxi Rental ERP System — a Clean Architecture ASP.NET Core / EF Core / MySQL modular monolith that manages investors, vehicles, drivers, contracts, invoices, payments, and a double-entry-style accounting/transaction engine. Use this skill whenever the user asks to scaffold, design, implement, review, or extend ANY part of this specific ERP project — including things phrased generically like "add a new module", "create the database schema", "build the investor CRUD", "design the accounting engine", "add a new phase", "set up the .NET solution", "write the domain entities", or references to CLAUDE.md, the fleet/taxi rental ERP, investor/driver/vehicle ledgers, or the phased development plan (Phase 0 through Phase 12+). Always consult this skill before writing any code for this project, even for small additions, so the result stays consistent with the established architecture, folder structure, naming, and business rules.
---

# Fleet Management & Taxi Rental ERP (.NET)

Use this skill to generate code, schema, docs, or plans for this exact project — a
Clean Architecture, Modular Monolith ERP built with ASP.NET Core + EF Core + MySQL,
for companies that manage investor-owned taxi fleets rented out to drivers.

The company **never owns vehicles**. It manages them on behalf of investors and
automates the full lifecycle: investor onboarding → vehicle assignment → driver
contracts → invoicing → payments → accounting → reporting.

## Core operating principles (apply to everything you generate)

1. **Simplicity over completeness.** Build the simplest production-ready version of
   whatever is asked. Never add features, entities, or abstractions that weren't
   requested or aren't implied by the business rules below.
2. **Explicit over clever.** Prefer readable, boring code over generic/reflection-heavy
   abstractions. No premature interfaces "just in case."
3. **Everything financial goes through the transaction engine.** Never let any code
   path update an Account balance directly — always via a Transaction (see
   `references/accounting-engine.md`).
4. **Nothing is hardcoded that the business will want to change.** Statuses, payment
   methods, expense categories, vehicle/fuel/transmission types, document types,
   notification types, permissions, roles, menus — all come from the database as
   dynamic lookups, not enums baked into code (see `references/architecture.md`
   → "Dynamic Lookup Architecture").
5. **Never delete contracts, invoices, or financial records.** Use status transitions
   (Cancelled, Terminated, Voided) instead of hard deletes. Soft-delete/audit
   everywhere financial or contractual.
6. **Preserve the existing frontend/dashboard.** The dashboard is server-rendered
   ASP.NET Core MVC / Razor views (no SPA, no JS framework). If asked for frontend
   work, extend the existing Razor views and layout in the presentation project —
   don't introduce a new design system or client framework. The presentation project
   (`FleetErp.Api`) hosts both the API controllers and the MVC/Razor views +
   `wwwroot`.
7. **Follow the phased build order** unless the user explicitly asks to jump ahead
   (see `references/phases.md`). Don't build Phase 6 (Contracts) plumbing before
   Phase 1 (Auth/RBAC) exists, for example — call this out if the user's request
   would violate dependency order, but still help if they insist.

## Before writing any code

1. Check `references/architecture.md` for the solution/folder structure, layering
   rules, and required patterns (Repository, Unit of Work, Service Layer, DI, CQRS
   usage policy).
2. Check `references/domain-model.md` for the four core entities (Investor, Vehicle,
   Driver, Company), their relationships, contract/invoice/payment rules, and the
   full dynamic-lookup catalogue.
3. Check `references/accounting-engine.md` for Account types, Transaction flow
   diagrams, and the invariant that balances are **derived**, never mutated directly.
4. Check `references/phases.md` to know what should already exist vs. what's new for
   the phase being worked on, and what each phase's deliverable is.
5. Check `references/code-templates.md` for the canonical C# skeletons (BaseEntity,
   Result<T>, UoW, repository, entity-with-invariant, EF config, service,
   controller, validator, DI module) — copy these shapes rather than inventing new
   ones, so every module looks the same.
6. Check `references/database-schema.md` for concrete table definitions of the core
   and Phase 1–3 tables before writing entities or migrations.
7. If the ask is genuinely new/ambiguous (a feature not implied by the business
   rules above), ask one clarifying question rather than guessing — but don't ask if
   the answer is inferable from the documents.

## Tech stack (fixed — do not substitute)

| Layer | Choice |
|---|---|
| Backend | ASP.NET Core (.NET, latest LTS) |
| ORM | Entity Framework Core |
| Database | MySQL |
| Auth | JWT + Role-Based Access Control, permissions database-driven |
| Frontend | ASP.NET Core MVC / Razor views (server-rendered) — the existing Dashboard project. Extend it, never redesign; no SPA/JS framework. |
| Architecture | Clean Architecture, Modular Monolith |
| Patterns | Repository, Unit of Work, Service Layer, DI. CQRS **only** if a module's read/write complexity genuinely justifies it — default to simple service methods. |

## Solution structure (summary — full detail in references/architecture.md)

```
FleetErp.sln
├── src/
│   ├── FleetErp.Domain/            # Entities, value objects, enums-as-lookups, domain events
│   ├── FleetErp.Application/       # Interfaces, DTOs, services, validators (per module)
│   ├── FleetErp.Infrastructure/    # EF Core DbContext, Migrations, Repositories, UoW, external services
│   ├── FleetErp.Api/               # Controllers, Middleware, Auth, DI composition root
│   └── FleetErp.Shared/            # Cross-cutting: Result<T>, exceptions, constants
└── tests/
    ├── FleetErp.UnitTests/
    └── FleetErp.IntegrationTests/
```

Each business module (Investors, Vehicles, Drivers, Contracts, Invoices, Payments,
Accounting, Maintenance, Insurance) gets its own folder inside
Domain/Application/Infrastructure, not its own project — this is a **modular
monolith**, not microservices. Keep module boundaries clean via interfaces so any
module could be extracted later without a rewrite.

## When generating a new module, always produce (in this order)

1. **Domain entities** (Domain layer) — properties, relationships, invariants enforced
   in the entity itself where possible (e.g., an Invoice can't go from `Paid` back to
   `Pending`).
2. **EF Core configuration** (Infrastructure/Persistence/Configurations) — fluent API,
   not data annotations, for anything beyond the basics.
3. **Migration** — never hand-edit generated migrations; regenerate if the model
   changes.
4. **Repository interface + implementation** — one per aggregate root, not per table.
5. **Service layer** — orchestrates repositories + Unit of Work; this is where
   business rules and transaction-engine calls live. Controllers must stay thin.
6. **DTOs + validators** (Application layer) — never expose domain entities directly
   through the API.
7. **Controller** (Api layer) — thin, maps DTO ↔ service calls, applies `[Authorize]`
   with the correct database-driven permission.
8. **Audit log hook** — any create/update/delete/status-change on a business entity
   must write an Audit Log entry (see `references/domain-model.md` → Audit Log).

## What NOT to do

- Don't invent new top-level entities beyond Investor / Vehicle / Driver / Company
  and their documented sub-concepts, unless the user explicitly asks for one.
- Don't hardcode status/type lists as C# enums if they're in the "Dynamic System"
  list in `references/domain-model.md` — use lookup tables instead.
- Don't bypass the Account/Transaction model with direct balance columns updated via
  `SaveChanges` outside the transaction engine.
- Don't redesign the dashboard or introduce a new frontend framework.
- Don't reach for CQRS/MediatR by default — only when a module's justified.
- Don't delete contracts or financial rows — status/soft-delete only.

## Reference files

- `references/architecture.md` — full Clean Architecture layout, layer
  responsibilities, DI conventions, dynamic lookup pattern, RBAC/permission model,
  audit logging pattern, notification pattern.
- `references/domain-model.md` — full entity catalogue (Investor, Vehicle, Driver,
  Company), Contract/Invoice/Payment rules, the complete list of dynamic
  statuses/types, security roles, dashboard widgets, and reports.
- `references/accounting-engine.md` — Account types and the five canonical
  Transaction flows (Invoice, Payment, Maintenance Expense, Investor Withdrawal,
  Bank Transfer) with the invariant that balances are always derived from
  Transactions.
- `references/phases.md` — the 12+ phase build order with deliverables per phase, so
  new work can be placed correctly and dependencies respected.
- `references/code-templates.md` — canonical, copy-pasteable C# skeletons and the
  pinned conventions (Result-vs-exceptions, API envelope, pagination, async naming,
  per-module "definition of done"). Use these to keep every module structurally
  identical.
- `references/database-schema.md` — concrete MySQL table definitions for the
  foundational + Phase 1–3 tables, plus schema rules (status columns are FKs to
  lookups, money is DECIMAL(18,2), soft-delete for financial rows,
  payment_allocations for multi-invoice payments).

## Typical requests this skill should handle

- "Set up the solution / Phase 0 foundation" → scaffold folder structure per
  `references/architecture.md`, empty DbContext, DI composition, no business logic yet.
- "Build the Investor module" → follow the 8-step module checklist above using
  `references/domain-model.md` → Investors.
- "Design the database schema for X" → start from the concrete tables in
  `references/database-schema.md`; for anything not yet defined there, derive it from
  `references/domain-model.md` following the same conventions (status/type columns
  are `*_id` FKs to lookups, money is DECIMAL(18,2), financial rows soft-delete).
- "How should a payment update the ledger?" → walk through
  `references/accounting-engine.md` → Payment Received flow.
- "What's next after Phase 5?" → `references/phases.md` → Phase 6, Contracts.
- "Write CLAUDE.md for this project" → synthesize the project summary the user
  already has (this skill's content) plus any schema/decisions made so far in the
  conversation.
