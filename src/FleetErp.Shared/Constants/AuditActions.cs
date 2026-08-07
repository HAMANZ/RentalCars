namespace FleetErp.Shared.Constants;

/// <summary>
/// Standard audit action names for consistency across the system.
/// </summary>
public static class AuditActions
{
    public const string Created = "Created";
    public const string Updated = "Updated";
    public const string Deleted = "Deleted";
    public const string StatusChanged = "StatusChanged";
    public const string Activated = "Activated";
    public const string Deactivated = "Deactivated";
    public const string Approved = "Approved";
    public const string Rejected = "Rejected";
    public const string Cancelled = "Cancelled";
    public const string Renewed = "Renewed";
    public const string Terminated = "Terminated";
    public const string PaymentReceived = "PaymentReceived";
    public const string PaymentVoided = "PaymentVoided";
    public const string InvoiceGenerated = "InvoiceGenerated";
    public const string InvoiceVoided = "InvoiceVoided";
    public const string WithdrawalRequested = "WithdrawalRequested";
    public const string WithdrawalProcessed = "WithdrawalProcessed";
    public const string Login = "Login";
    public const string Logout = "Logout";
    public const string PasswordChanged = "PasswordChanged";
    public const string PasswordReset = "PasswordReset";

    // User actions
    public const string UserCreated = "UserCreated";
    public const string UserUpdated = "UserUpdated";
    public const string UserDeleted = "UserDeleted";
    public const string UserActivated = "UserActivated";
    public const string UserDeactivated = "UserDeactivated";

    // Role actions
    public const string RoleCreated = "RoleCreated";
    public const string RoleUpdated = "RoleUpdated";
    public const string RoleDeleted = "RoleDeleted";
    public const string RolePermissionsUpdated = "RolePermissionsUpdated";

    // Investor actions
    public const string InvestorCreated = "InvestorCreated";
    public const string InvestorUpdated = "InvestorUpdated";
    public const string InvestorDeleted = "InvestorDeleted";
    public const string InvestorDocumentAdded = "InvestorDocumentAdded";
    public const string InvestorDocumentDeleted = "InvestorDocumentDeleted";

    // Vehicle actions
    public const string VehicleCreated = "VehicleCreated";
    public const string VehicleUpdated = "VehicleUpdated";
    public const string VehicleDeleted = "VehicleDeleted";
    public const string VehicleDocumentAdded = "VehicleDocumentAdded";
    public const string VehicleDocumentDeleted = "VehicleDocumentDeleted";

    // Customer actions
    public const string CustomerCreated = "CustomerCreated";
    public const string CustomerUpdated = "CustomerUpdated";
    public const string CustomerDeleted = "CustomerDeleted";
    public const string CustomerDocumentAdded = "CustomerDocumentAdded";
    public const string CustomerDocumentDeleted = "CustomerDocumentDeleted";

    // Rental actions
    public const string RentalCreated = "RentalCreated";
    public const string RentalUpdated = "RentalUpdated";
    public const string RentalDeleted = "RentalDeleted";
    public const string RentalCompleted = "RentalCompleted";
    public const string RentalCancelled = "RentalCancelled";
    public const string RentalPaymentAdded = "RentalPaymentAdded";
    public const string RentalPaymentDeleted = "RentalPaymentDeleted";

    // Maintenance actions
    public const string MaintenanceCreated = "MaintenanceCreated";
    public const string MaintenanceUpdated = "MaintenanceUpdated";
    public const string MaintenanceDeleted = "MaintenanceDeleted";
    public const string MaintenanceStarted = "MaintenanceStarted";
    public const string MaintenanceCompleted = "MaintenanceCompleted";
    public const string MaintenanceCancelled = "MaintenanceCancelled";
    public const string MaintenanceExpensePosted = "MaintenanceExpensePosted";

    // Insurance actions
    public const string InsuranceCreated = "InsuranceCreated";
    public const string InsuranceUpdated = "InsuranceUpdated";
    public const string InsuranceDeleted = "InsuranceDeleted";
    public const string InsuranceRenewed = "InsuranceRenewed";
    public const string InsuranceCancelled = "InsuranceCancelled";
    public const string InsuranceCompanyCreated = "InsuranceCompanyCreated";
    public const string InsuranceCompanyUpdated = "InsuranceCompanyUpdated";
    public const string InsuranceCompanyDeleted = "InsuranceCompanyDeleted";
}
