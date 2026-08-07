# Domain Model — Fleet Management & Taxi Rental ERP

## The four core entities

### Investors
Investors own vehicles (one or many). They receive revenue according to company
rules and never touch operations directly.

Each Investor has: Profile, Documents, Contracts, Vehicles, Ledger, Account,
Transactions, Reports.

### Vehicles
Vehicles belong to Investors. A vehicle has: Current Driver, History, Maintenance,
Insurance, Documents, Expenses, Revenue, Operational Ledger. A vehicle may have
multiple Contracts over its lifetime (one active at a time, many historical).

### Drivers
Drivers rent vehicles. Each Driver has: Personal Information, Documents, Contract
History, Invoices, Payments, Ledger, Account, Violations, Notes.

### Company
The company manages Investors, Vehicles, Drivers, Contracts, Accounting,
Maintenance, and Insurance. It is not a literal DB entity so much as the umbrella
the Company Account and cross-cutting reports roll up to.

## Relationships

```
Investor → Vehicle → Driver → Contract → Invoices → Payments
```

- An Investor owns many Vehicles.
- A Vehicle has one *current* Driver (via the active Contract) and many historical
  Drivers over its lifetime.
- A Contract belongs to exactly one Driver and one Vehicle.
- A Contract may generate multiple Invoices.
- An Invoice may receive multiple Payments (partial payments); a single Payment may
  also pay multiple Invoices.

## Contracts

- Belongs to one Driver, belongs to one Vehicle.
- May generate multiple Invoices over its life.
- Stores historical information (never overwritten).
- Can be renewed (creates continuity, not a new unrelated contract — link renewals).
- Can be terminated (status change, not delete).
- **Never delete a contract.** Ever.

## Invoices

Statuses (dynamic, not hardcoded): Pending, Partially Paid, Paid, Overdue,
Cancelled. Status must be computed/updated dynamically as payments arrive, not set
once and forgotten.

Invoices support: Discounts, Notes, Attachments.

## Payments

A single payment may:
- Pay one invoice in full.
- Pay multiple invoices.
- Partially pay an invoice, leaving a remaining balance.
- Use different payment methods (dynamic lookup, e.g. cash, bank transfer, card).

Every payment must automatically cascade updates to: the Invoice(s) it touches, the
relevant Account, the Ledger, the Dashboard, and the Audit Log. This cascade should
live in the Service layer / transaction engine, never split across ad-hoc controller
logic.

## Dynamic system — everything below comes from the database, not C# enums

**Statuses:** Vehicle, Invoice, Payment, Contract, Driver, Investor, Maintenance,
Insurance, Expense, Notification.

**Type/category lookups:** Payment Methods, Expense Categories, Maintenance Types,
Vehicle Types, Fuel Types, Transmission Types, Document Types, Insurance Companies,
Notification Types.

**UI/config lookups:** Dashboard Widgets, Dashboard Cards, Menus, Sidebar,
Permissions, Roles.

Practical rule: if it's in this list, model it as a lookup table
(`Id, Code, Name, IsActive, SortOrder, ...`) with a FK from the owning entity, not as
a hardcoded enum or magic string. New values must be addable from an admin screen
without a deploy.

## Security / RBAC

- Authentication: JWT.
- Authorization: Role-Based Access Control, database-driven roles and permissions.
- Example roles: Administrator, Manager, Accountant, Receptionist, Maintenance
  Employee — but roles themselves are data, not a fixed C# enum, so custom roles can
  be added later.
- Example permission actions: Read, Create, Update, Delete, Approve, Export, Print.
  Permissions are assigned to roles, roles to users, all via DB tables.

## Audit Log

Every important action must create an Audit Log entry. Examples: User Login, Invoice
Created, Payment Received, Contract Updated, Maintenance Added, Insurance Renewed,
Expense Added, Settings Updated, Status Changed.

Implementation guidance: put this behind a Service-layer hook (e.g. an
`IAuditLogger` called explicitly at the end of each service method that mutates
state, or an EF Core `SaveChanges` interceptor for a safety net) — favor the
explicit service-layer call for anything financial so the audit entry can capture
business-meaningful context (not just "row changed"), and use the interceptor only
as a catch-all for entities that wouldn't otherwise get one.

## Notifications

Automatic notifications for: Insurance Expiration, Maintenance Due, Oil Change,
License Expiration, Contract Expiration, Late Payment, Invoice Due, Vehicle
Inspection. The Dashboard displays pending notifications. These are best implemented
as a scheduled job (e.g. a background worker) that scans for upcoming
expirations/due dates and writes Notification rows, rather than computed live on
every dashboard load.

## Dashboard (preserve existing layout — extend only)

Should surface: Vehicles, Drivers, Investors, Invoices, Payments, Cash, Revenue,
Expenses, Late Payments, Maintenance, Insurance, Upcoming Expirations, Recent
Transactions, Quick Statistics, Charts, Latest Activities.

## Reports

Investor, Driver, Vehicle, Contract, Invoice, Payment, Maintenance, Insurance,
Revenue, Expense, Ledger, Cashbox, Audit reports.

## Future features (do not build now, but don't architect against them)

Multi-Branch, GPS Tracking, Mobile App, Customer/Investor/Driver Portals, Accounting
Export, SMS/Email/WhatsApp Notifications, Vehicle Live Tracking, Payroll, Tax
Management, public API.

Keep this in mind when naming things and choosing extension points (e.g., a
`BranchId` nullable FK sprinkled in now costs little; building a full multi-tenant
system now would violate the YAGNI principle in the SKILL.md).
