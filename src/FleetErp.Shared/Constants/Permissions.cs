namespace FleetErp.Shared.Constants;

/// <summary>
/// Permission constants for the RBAC system.
/// Format: Module.Action
/// These are seeded into the database and assigned to roles.
/// </summary>
public static class Permissions
{
    public static class Users
    {
        public const string View = "Users.View";
        public const string Create = "Users.Create";
        public const string Edit = "Users.Edit";
        public const string Delete = "Users.Delete";
        public const string ResetPassword = "Users.ResetPassword";
    }

    public static class Roles
    {
        public const string View = "Roles.View";
        public const string Create = "Roles.Create";
        public const string Edit = "Roles.Edit";
        public const string Delete = "Roles.Delete";
        public const string AssignPermissions = "Roles.AssignPermissions";
    }

    public static class Investors
    {
        public const string View = "Investors.View";
        public const string Create = "Investors.Create";
        public const string Edit = "Investors.Edit";
        public const string Delete = "Investors.Delete";
    }

    public static class Vehicles
    {
        public const string View = "Vehicles.View";
        public const string Create = "Vehicles.Create";
        public const string Edit = "Vehicles.Edit";
        public const string Delete = "Vehicles.Delete";
    }

    public static class Customers
    {
        public const string View = "Customers.View";
        public const string Create = "Customers.Create";
        public const string Edit = "Customers.Edit";
        public const string Delete = "Customers.Delete";
    }

    public static class Rentals
    {
        public const string View = "Rentals.View";
        public const string Create = "Rentals.Create";
        public const string Edit = "Rentals.Edit";
        public const string Delete = "Rentals.Delete";
        public const string Complete = "Rentals.Complete";
        public const string Cancel = "Rentals.Cancel";
        public const string AddPayment = "Rentals.AddPayment";
    }

    public static class Drivers
    {
        public const string View = "Drivers.View";
        public const string Create = "Drivers.Create";
        public const string Edit = "Drivers.Edit";
        public const string Delete = "Drivers.Delete";
    }

    public static class Contracts
    {
        public const string View = "Contracts.View";
        public const string Create = "Contracts.Create";
        public const string Edit = "Contracts.Edit";
        public const string Approve = "Contracts.Approve";
        public const string Terminate = "Contracts.Terminate";
    }

    public static class Invoices
    {
        public const string View = "Invoices.View";
        public const string Create = "Invoices.Create";
        public const string Void = "Invoices.Void";
    }

    public static class Payments
    {
        public const string View = "Payments.View";
        public const string Create = "Payments.Create";
        public const string Void = "Payments.Void";
    }

    public static class Maintenance
    {
        public const string View = "Maintenance.View";
        public const string Create = "Maintenance.Create";
        public const string Edit = "Maintenance.Edit";
    }

    public static class Insurance
    {
        public const string View = "Insurance.View";
        public const string Create = "Insurance.Create";
        public const string Edit = "Insurance.Edit";
    }

    public static class Reports
    {
        public const string View = "Reports.View";
        public const string Export = "Reports.Export";
    }

    public static class Settings
    {
        public const string View = "Settings.View";
        public const string Edit = "Settings.Edit";
    }

    public static class Lookups
    {
        public const string Manage = "Lookups.Manage";
    }

    /// <summary>
    /// Get all permission codes as a flat list (for seeding).
    /// </summary>
    public static IEnumerable<(string Code, string Name, string Module)> GetAll()
    {
        yield return (Users.View, "View Users", "Users");
        yield return (Users.Create, "Create Users", "Users");
        yield return (Users.Edit, "Edit Users", "Users");
        yield return (Users.Delete, "Delete Users", "Users");
        yield return (Users.ResetPassword, "Reset User Password", "Users");

        yield return (Roles.View, "View Roles", "Roles");
        yield return (Roles.Create, "Create Roles", "Roles");
        yield return (Roles.Edit, "Edit Roles", "Roles");
        yield return (Roles.Delete, "Delete Roles", "Roles");
        yield return (Roles.AssignPermissions, "Assign Permissions to Roles", "Roles");

        yield return (Investors.View, "View Investors", "Investors");
        yield return (Investors.Create, "Create Investors", "Investors");
        yield return (Investors.Edit, "Edit Investors", "Investors");
        yield return (Investors.Delete, "Delete Investors", "Investors");

        yield return (Vehicles.View, "View Vehicles", "Vehicles");
        yield return (Vehicles.Create, "Create Vehicles", "Vehicles");
        yield return (Vehicles.Edit, "Edit Vehicles", "Vehicles");
        yield return (Vehicles.Delete, "Delete Vehicles", "Vehicles");

        yield return (Customers.View, "View Customers", "Customers");
        yield return (Customers.Create, "Create Customers", "Customers");
        yield return (Customers.Edit, "Edit Customers", "Customers");
        yield return (Customers.Delete, "Delete Customers", "Customers");

        yield return (Rentals.View, "View Rentals", "Rentals");
        yield return (Rentals.Create, "Create Rentals", "Rentals");
        yield return (Rentals.Edit, "Edit Rentals", "Rentals");
        yield return (Rentals.Delete, "Delete Rentals", "Rentals");
        yield return (Rentals.Complete, "Complete Rentals", "Rentals");
        yield return (Rentals.Cancel, "Cancel Rentals", "Rentals");
        yield return (Rentals.AddPayment, "Add Rental Payments", "Rentals");

        yield return (Drivers.View, "View Drivers", "Drivers");
        yield return (Drivers.Create, "Create Drivers", "Drivers");
        yield return (Drivers.Edit, "Edit Drivers", "Drivers");
        yield return (Drivers.Delete, "Delete Drivers", "Drivers");

        yield return (Contracts.View, "View Contracts", "Contracts");
        yield return (Contracts.Create, "Create Contracts", "Contracts");
        yield return (Contracts.Edit, "Edit Contracts", "Contracts");
        yield return (Contracts.Approve, "Approve Contracts", "Contracts");
        yield return (Contracts.Terminate, "Terminate Contracts", "Contracts");

        yield return (Invoices.View, "View Invoices", "Invoices");
        yield return (Invoices.Create, "Create Invoices", "Invoices");
        yield return (Invoices.Void, "Void Invoices", "Invoices");

        yield return (Payments.View, "View Payments", "Payments");
        yield return (Payments.Create, "Create Payments", "Payments");
        yield return (Payments.Void, "Void Payments", "Payments");

        yield return (Maintenance.View, "View Maintenance", "Maintenance");
        yield return (Maintenance.Create, "Create Maintenance Records", "Maintenance");
        yield return (Maintenance.Edit, "Edit Maintenance Records", "Maintenance");

        yield return (Insurance.View, "View Insurance", "Insurance");
        yield return (Insurance.Create, "Create Insurance Policies", "Insurance");
        yield return (Insurance.Edit, "Edit Insurance Policies", "Insurance");

        yield return (Reports.View, "View Reports", "Reports");
        yield return (Reports.Export, "Export Reports", "Reports");

        yield return (Settings.View, "View Settings", "Settings");
        yield return (Settings.Edit, "Edit Settings", "Settings");

        yield return (Lookups.Manage, "Manage Lookups", "Lookups");
    }
}
