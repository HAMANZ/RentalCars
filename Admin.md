# Rental cars and maintenance system architecture
RentalCars
│
├── DomainLayer
│   └── Models
│   └── DTO
│
├── ServiceLayer
│   ├── Helper
│   ├── Interfaces
│   └── Services
│
├── RepositoryLayer
│   └── RentalCarDbContext
│   └── migration
│
└── RentalCar
    ├── Controllers
    ├── Views
    │   ├── Shared
    │   │   ├── _AdminLayout.cshtml
    │   │   ├── _AdminSidebar.cshtml
    │   │   ├── _AdminNavbar.cshtml
    │   │   └── _AdminFooter.cshtml
    │   │
    │   ├── Dashboard
    │   ├── Cars
    │   ├── Customers
    │   ├── RentalContracts
    │   ├── Maintenance
    │   ├── Insurance
    │   ├── Accidents
    │   ├── Violations
    │   └── Accounting
    │
    └── wwwroot
        ├── adminlte
        ├── css
        └── js
		
		
#AdminLTE menu for your Rental Cars system


Dashboard
│
├── Fleet Management
│   ├── Cars
│   ├── Brands
│   ├── Fuel Types
│   ├── Car Owners
│   ├── License Plates
│   └── Investors
│
├── Rental Management
│   ├── Customers
│   ├── Rental Contracts
│   ├── Rental Payments
│   └── Branches
│
├── Maintenance
│   ├── Work Orders
│   ├── Repairs
│   ├── Spare Parts
│   ├── Suppliers
│   ├── Oil Change
│   ├── Tires
│   └── Batteries
│
├── Insurance & Documents
│   ├── Insurance
│   ├── Insurance Companies
│   ├── Insurance Types
│   ├── Inspections
│   ├── Car Documents
│   └── Document Types
│
├── Accidents & Violations
│   ├── Accidents
│   └── Violations
│
├── Accounting
│   ├── Accounts
│   ├── Account Types
│   ├── Transactions
│   ├── STransactions
│   └── Payment Methods
│
├── Administration
│   ├── Users
│   ├── Roles
│   ├── Languages
│   ├── Lookups
│   ├── Notifications
│   └── App Settings
│
└── Reports
    ├── Rental Reports
    ├── Maintenance Reports
    ├── Financial Reports
    └── Vehicle Profitability
	
	
	
	Integrate AdminLTE into the existing ASP.NET Core MVC RentalCars project.

Requirements:

1. Use the existing project architecture.
2. Do not replace or recreate the existing Domain, Services, DTOs, Validators, or DbContext.
3. Create an AdminLTE-based responsive admin dashboard.
4. Use the existing Controllers and Services.
5. Create a shared _Layout.cshtml.
6. Create:
   - _Navbar.cshtml
   - _Sidebar.cshtml
   - _Footer.cshtml
   - _ControlSidebar.cshtml if needed
7. Create a Dashboard page.

Create the sidebar according to these modules:

Dashboard

Fleet Management:
- Cars
- Brands
- Fuel Types
- Car Owners
- License Plates
- Investors

Rental Management:
- Customers
- Rental Contracts
- Rental Payments
- Branches

Maintenance:
- Work Orders
- Repairs
- Spare Parts
- Suppliers
- Oil Change
- Tires
- Batteries

Insurance & Documents:
- Insurance
- Insurance Companies
- Insurance Types
- Inspections
- Car Documents
- Document Types

Accidents & Violations:
- Accidents
- Violations

Accounting:
- Accounts
- Account Types
- Transactions
- STransactions
- Payment Methods

Administration:
- Users
- Roles
- Languages
- Lookups
- Notifications
- App Settings

Reports:
- Rental Reports
- Maintenance Reports
- Financial Reports
- Vehicle Profitability

#Important:

- Inspect the existing project before making changes.
- Use the actual controller names and action names.
- Do not invent routes.
- Do not change existing models.
- Do not change existing DbSet names.
- Reuse existing DTOs and Services.
- Use ASP.NET Core MVC Razor Views.
- Make the UI responsive.
- Support Arabic RTL and English LTR.
- Add localization-ready navigation text.
- Use Font Awesome icons where appropriate.
- Use AdminLTE cards, tables, badges, alerts, and dashboard widgets.
- Keep the code clean and modular.
- Build the project after implementation and fix compilation errors.


#Phase 1 — AdminLTE Shell + Placeholders

Build only the AdminLTE UI shell for the existing ASP.NET Core MVC project.

Scope:
- AdminLTE layout
- Navbar
- Sidebar
- Footer
- Dashboard page
- Navigation links

IMPORTANT:
- First inspect the existing solution.
- Inspect all existing Controllers and their Actions.
- Inspect existing Views and Areas.
- Do NOT invent controllers, actions, routes, or URLs.
- Do NOT create CRUD pages yet.
- Do NOT modify Domain Models.
- Do NOT modify the DbContext.
- Do NOT modify Services or DTOs.
- Keep this phase small and safe.

Navigation rules:

1. If a controller/action already exists:
   - Link the AdminLTE menu item to the REAL existing route/action.
   - Use ASP.NET Core MVC tag helpers such as:
     asp-area
     asp-controller
     asp-action

2. If a module does NOT have an existing controller/action:
   - Use href="#"
   - Do NOT create a controller just to make the link work.

3. Do not guess route names.

Existing modules should be discovered from the solution, including things such as:
- Admin/Users
- Menu
- Announcements
- Notifications
- App Settings
- Languages
- Lookups
- Customers
- Cars
- Rental Contracts
- Maintenance
- Insurance
- Accounting
etc.

Only connect modules where the corresponding controller/action actually exists.

AdminLTE shell:

Create/update only the necessary files for:

- _Layout.cshtml
- _Navbar.cshtml if appropriate
- _Sidebar.cshtml if appropriate
- _Footer.cshtml if appropriate
- Dashboard/Index.cshtml

Keep the existing project structure and namespaces.

Dashboard:
Create a simple AdminLTE dashboard with safe placeholder widgets/cards.
Do not query or invent database data unless an existing service/action already provides it.

Use simple static dashboard cards where real dashboard data does not exist.

Sidebar:
Organize the navigation into logical groups:

Dashboard

Fleet Management
- Cars
- Brands
- Fuel Types
- Car Owners
- License Plates
- Investors

Rental Management
- Customers
- Rental Contracts
- Rental Payments
- Branches

Maintenance
- Work Orders
- Repairs
- Spare Parts
- Suppliers
- Oil Change
- Tires
- Batteries

Insurance & Documents
- Insurance
- Insurance Companies
- Insurance Types
- Inspections
- Car Documents
- Document Types

Accidents & Violations
- Accidents
- Violations

Accounting
- Accounts
- Account Types
- Transactions
- STransactions
- Payment Methods

Administration
- Users
- Menu
- Announcements
- Notifications
- Languages
- Lookups
- App Settings

Reports
- Rental Reports
- Maintenance Reports
- Financial Reports
- Vehicle Profitability

For every menu item:
- Existing route → real MVC link
- No existing route → "#"

UI requirements:
- Responsive AdminLTE layout
- Font Awesome icons if already available
- Active menu item highlighting
- Collapsible sidebar
- Navbar
- Footer
- Breadcrumbs where appropriate
- Clean Razor code
- No duplicated markup where partials are appropriate

Arabic/English:
- Do not hard-code a new localization system.
- Reuse the existing localization mechanism if one already exists.
- Preserve existing Arabic/English support.
- Do not break RTL/LTR behavior.

Before changing anything:
1. Inspect the solution structure.
2. List the existing Controllers and Actions.
3. Identify the existing layout and shared views.
4. Identify the existing Admin area/routes.
5. Then implement the smallest safe change.

After implementation:
1. Build the solution.
2. Fix compilation errors caused by your changes.
3. Do not make unrelated refactoring.
4. Do not create routes that do not already exist.
5. Give me a summary of:
   - Files created
   - Files modified
   - Existing routes connected
   - Placeholder links
   - Build result

		