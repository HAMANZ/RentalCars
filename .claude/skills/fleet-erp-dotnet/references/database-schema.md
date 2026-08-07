# Database Schema (core tables)

Concrete table definitions for the foundational + Phase 1–3 tables, so code
generation is deterministic. MySQL conventions: snake_case table/column names,
`INT AUTO_INCREMENT` PKs, `DECIMAL(18,2)` for money, `DATETIME` timestamps, `utf8mb4`.

Every table below implicitly includes the BaseEntity audit columns:
`created_at DATETIME NOT NULL`, `created_by VARCHAR(100) NULL`,
`updated_at DATETIME NULL`, `updated_by VARCHAR(100) NULL`,
`is_deleted TINYINT(1) NOT NULL DEFAULT 0`.

## Dynamic lookups (Phase 2)

Default to a single shared lookup table; only break out a dedicated table when a
lookup needs extra columns (see `insurance_companies` below for that exception).

```
lookup_items
  id            INT PK
  lookup_type   VARCHAR(50)  NOT NULL   -- 'InvoiceStatus','VehicleStatus','FuelType','PaymentMethod',...
  code          VARCHAR(50)  NOT NULL
  name          VARCHAR(150) NOT NULL
  sort_order    INT          NOT NULL DEFAULT 0
  is_active     TINYINT(1)   NOT NULL DEFAULT 1
  UNIQUE (lookup_type, code)
```

`lookup_type` values to seed: InvoiceStatus, PaymentStatus, ContractStatus,
VehicleStatus, DriverStatus, InvestorStatus, MaintenanceStatus, InsuranceStatus,
ExpenseStatus, NotificationStatus, PaymentMethod, ExpenseCategory, MaintenanceType,
VehicleType, FuelType, TransmissionType, DocumentType, NotificationType,
AccountType, TransactionType.

```
insurance_companies         -- dedicated table (needs contact fields the shared lookup lacks)
  id            INT PK
  name          VARCHAR(150) NOT NULL
  phone         VARCHAR(50)  NULL
  email         VARCHAR(150) NULL
  is_active     TINYINT(1)   NOT NULL DEFAULT 1

countries  (id, name, iso_code)
cities     (id, country_id FK, name)
currencies (id, code, name, symbol)
```

## Security / RBAC (Phase 1)

```
users
  id            INT PK
  full_name     VARCHAR(150) NOT NULL
  email         VARCHAR(150) NOT NULL UNIQUE
  password_hash VARCHAR(255) NOT NULL
  is_active     TINYINT(1)   NOT NULL DEFAULT 1

roles         (id, name UNIQUE, description)
permissions   (id, code UNIQUE)         -- e.g. 'Payments.Create','Invoices.Approve'
user_roles       (user_id FK, role_id FK, PK(user_id, role_id))
role_permissions (role_id FK, permission_id FK, PK(role_id, permission_id))

refresh_tokens
  id            INT PK
  user_id       INT FK
  token         VARCHAR(255) NOT NULL
  expires_at    DATETIME     NOT NULL
  revoked_at    DATETIME     NULL

menus            (id, parent_id FK NULL, name, route, icon, sort_order, permission_id FK NULL)
audit_logs
  id            INT PK
  user_id       INT NULL
  action        VARCHAR(100) NOT NULL      -- 'PaymentReceived','ContractUpdated',...
  entity_type   VARCHAR(100) NULL
  entity_id     INT          NULL
  details       TEXT         NULL
  occurred_at   DATETIME     NOT NULL
```

## Accounting core (Phase 9, but tables exist early — Invoice Created already posts)

```
accounts
  id              INT PK
  account_type_id INT FK  -> lookup_items(AccountType)
  owner_type      VARCHAR(50) NOT NULL    -- 'Investor','Driver','Vehicle','Company','Cashbox','Bank',...
  owner_id        INT NULL                 -- FK to owning entity; NULL for Company/Cashbox/Bank singletons
  balance         DECIMAL(18,2) NOT NULL DEFAULT 0   -- mutated ONLY by the transaction engine
  currency        VARCHAR(10) NOT NULL
  INDEX (owner_type, owner_id)

transactions
  id                INT PK
  transaction_type_id INT FK -> lookup_items(TransactionType)
  debit_account_id  INT FK -> accounts(id)
  credit_account_id INT FK -> accounts(id)
  amount            DECIMAL(18,2) NOT NULL
  reference_type    VARCHAR(50) NULL       -- 'Invoice','Payment','MaintenanceExpense',...
  reference_id      INT NULL
  occurred_at       DATETIME NOT NULL
  notes             VARCHAR(500) NULL
  INDEX (debit_account_id), INDEX (credit_account_id), INDEX (reference_type, reference_id)
```

## Investors (Phase 3)

```
investors
  id            INT PK
  full_name     VARCHAR(150) NOT NULL
  phone         VARCHAR(50)  NULL
  email         VARCHAR(150) NULL
  national_id   VARCHAR(50)  NULL
  status_id     INT FK -> lookup_items(InvestorStatus)
  account_id    INT FK -> accounts(id)      -- each investor owns one Account

investor_documents
  id            INT PK
  investor_id   INT FK
  document_type_id INT FK -> lookup_items(DocumentType)
  file_path     VARCHAR(500) NOT NULL
  expires_at    DATETIME NULL
```

## Vehicles (Phase 4)

```
vehicles
  id            INT PK
  investor_id   INT FK -> investors(id)     -- ownership
  plate_number  VARCHAR(30) NOT NULL UNIQUE
  make          VARCHAR(100) NULL
  model         VARCHAR(100) NULL
  year          INT NULL
  vehicle_type_id   INT FK -> lookup_items(VehicleType)
  fuel_type_id      INT FK -> lookup_items(FuelType)
  transmission_id   INT FK -> lookup_items(TransmissionType)
  status_id     INT FK -> lookup_items(VehicleStatus)
  account_id    INT FK -> accounts(id)      -- vehicle operational account
```

## Drivers (Phase 5)

```
drivers
  id            INT PK
  full_name     VARCHAR(150) NOT NULL
  phone         VARCHAR(50)  NULL
  national_id   VARCHAR(50)  NULL
  license_no    VARCHAR(50)  NULL
  license_expiry DATETIME NULL              -- feeds License Expiration notifications
  status_id     INT FK -> lookup_items(DriverStatus)
  account_id    INT FK -> accounts(id)
```

## Contracts / Invoices / Payments (Phases 6–8)

```
contracts
  id            INT PK
  vehicle_id    INT FK -> vehicles(id)
  driver_id     INT FK -> drivers(id)
  status_id     INT FK -> lookup_items(ContractStatus)
  start_date    DATETIME NOT NULL
  end_date      DATETIME NULL
  rent_amount   DECIMAL(18,2) NOT NULL
  renewed_from_contract_id INT NULL FK -> contracts(id)   -- links renewals; never delete a contract

invoices
  id            INT PK
  contract_id   INT FK -> contracts(id)
  status_id     INT FK -> lookup_items(InvoiceStatus)
  total_amount  DECIMAL(18,2) NOT NULL
  discount      DECIMAL(18,2) NOT NULL DEFAULT 0
  amount_paid   DECIMAL(18,2) NOT NULL DEFAULT 0
  due_date      DATETIME NULL              -- feeds Invoice Due / Overdue notifications
  notes         VARCHAR(500) NULL

invoice_attachments (id, invoice_id FK, file_path)

payments
  id                INT PK
  payment_method_id INT FK -> lookup_items(PaymentMethod)
  amount            DECIMAL(18,2) NOT NULL
  paid_at           DATETIME NOT NULL
  notes             VARCHAR(500) NULL

payment_allocations              -- lets one payment cover multiple invoices, or partial
  id            INT PK
  payment_id    INT FK -> payments(id)
  invoice_id    INT FK -> invoices(id)
  amount        DECIMAL(18,2) NOT NULL
```

## Notifications (Phase 12; table scaffolded earlier)

```
notifications
  id                INT PK
  notification_type_id INT FK -> lookup_items(NotificationType)
  status_id         INT FK -> lookup_items(NotificationStatus)
  title             VARCHAR(200) NOT NULL
  message           VARCHAR(500) NULL
  reference_type    VARCHAR(50) NULL
  reference_id      INT NULL
  due_at            DATETIME NULL
```

## Schema rules to enforce in generated code

- Status/type columns are always `*_id INT FK` to a lookup — never a free-text
  `VARCHAR` status.
- Money is always `DECIMAL(18,2)`; configure precision in EF fluent config.
- Financial/contractual rows soft-delete (`is_deleted`) — no hard deletes; enforce
  with an EF global query filter.
- `payment_allocations` is what makes "one payment → many invoices / partial pay"
  work — don't put a single `invoice_id` on `payments`.
