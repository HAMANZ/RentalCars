namespace FleetErp.Shared.Constants;

/// <summary>
/// Lookup type constants for the dynamic lookup system.
/// These are the LookupType values stored in the lookup_items table.
/// </summary>
public static class LookupTypes
{
    // Invoice & Payment
    public const string InvoiceStatus = "InvoiceStatus";
    public const string PaymentMethod = "PaymentMethod";
    public const string PaymentStatus = "PaymentStatus";

    // Vehicle
    public const string VehicleStatus = "VehicleStatus";
    public const string VehicleType = "VehicleType";
    public const string FuelType = "FuelType";
    public const string TransmissionType = "TransmissionType";

    // Contract
    public const string ContractStatus = "ContractStatus";
    public const string ContractType = "ContractType";

    // Driver
    public const string DriverStatus = "DriverStatus";

    // Investor
    public const string InvestorStatus = "InvestorStatus";

    // Customer
    public const string CustomerStatus = "CustomerStatus";

    // Rental
    public const string RentalStatus = "RentalStatus";

    // Maintenance
    public const string MaintenanceType = "MaintenanceType";
    public const string MaintenanceStatus = "MaintenanceStatus";

    // Insurance
    public const string InsuranceType = "InsuranceType";
    public const string InsuranceStatus = "InsuranceStatus";

    // Expense
    public const string ExpenseCategory = "ExpenseCategory";

    // Document
    public const string DocumentType = "DocumentType";

    // Notification
    public const string NotificationType = "NotificationType";
    public const string NotificationStatus = "NotificationStatus";

    // Accounting
    public const string AccountType = "AccountType";
    public const string TransactionType = "TransactionType";

    // Location
    public const string Country = "Country";
    public const string City = "City";
    public const string Currency = "Currency";
}
