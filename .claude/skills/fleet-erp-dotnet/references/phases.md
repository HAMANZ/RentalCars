# Development Phases

Build in this order unless the user explicitly asks to jump ahead. Each phase should
be a working, demoable increment — not a partial skeleton.

## Phase 0 — Foundation
Analyze requirements, design database, design architecture/folder structure,
configure solution, stub out authentication architecture, RBAC architecture, dynamic
lookup architecture, accounting architecture, transaction engine architecture.
**Deliverable:** a running empty project with the architecture from
`architecture.md` in place, no business features yet.

## Phase 1 — Authentication & Administration
Login, Logout, Refresh Token, Users, Roles, Permissions, Dynamic Menus, User
Profile, Audit Logs.
**Deliverable:** complete security system (JWT + DB-driven RBAC + audit logging
plumbing).

## Phase 2 — System Configuration
Settings, Statuses, Lookup Tables, Countries, Cities, Currencies, Payment Methods,
Expense Categories, Maintenance Types, Insurance Types, Notification Types, Vehicle
Types, Fuel Types, Transmission Types, Dashboard Settings, Company Settings.
**Deliverable:** a completely configurable system — every dynamic lookup from
`domain-model.md` is CRUD-able by an admin.

## Phase 3 — Investors
Investor CRUD, Documents, Vehicles (association), Contracts (association), Account,
Ledger, Transactions, Reports.
**Deliverable:** complete Investor management.

## Phase 4 — Vehicles
Vehicle CRUD, Ownership, Documents, Status, History, Maintenance Summary, Insurance
Summary, Account, Ledger.
**Deliverable:** complete Fleet management.

## Phase 5 — Drivers
Driver CRUD, Documents, Contracts, Vehicle Assignment, Ledger, Account,
Transactions, Reports, Notes.
**Deliverable:** complete Driver management.

## Phase 6 — Contracts
Investor → Vehicle → Driver → Contract relationships; renewal and termination
flows; contract history (never deleted).

## Phase 7 — Invoices
Invoice generation from contracts; dynamic status engine (Pending/Partially
Paid/Paid/Overdue/Cancelled); discounts, notes, attachments.

## Phase 8 — Payments
Single/multi-invoice payments, partial payments, payment methods; cascades to
Invoice, Account, Ledger, Dashboard, Audit Log.

## Phase 9 — Accounting Engine
Full implementation of `accounting-engine.md`: Accounts, Transactions, the five
canonical flows, ledger/report read paths.

## Phase 10 — Maintenance
Maintenance records tied to Vehicles, linked to Maintenance Expense transaction flow.

## Phase 11 — Insurance
Insurance records tied to Vehicles, expiration tracking feeding the Notifications
system.

## Phase 12 — Reports, Dashboard, Notifications
Full report set (`domain-model.md` → Reports), dashboard widgets/cards wired to
real data, background job for expiration/due-date notifications.

## Dependency notes
- Phases 3–5 (Investors/Vehicles/Drivers) can be built in parallel once Phase 2
  lookups exist, but Contracts (Phase 6) needs all three.
- Invoices (Phase 7) need Contracts (Phase 6).
- Payments (Phase 8) need Invoices (Phase 7) and the Accounting Engine (Phase 9) —
  in practice, build a minimal Accounting Engine alongside Phase 7/8 rather than
  strictly after, since Invoice Created already posts a transaction.
- Notifications (Phase 12) depend on Maintenance (10) and Insurance (11) existing
  for their expiration triggers, but the notification *infrastructure* (job runner,
  Notification table) can be scaffolded earlier.
