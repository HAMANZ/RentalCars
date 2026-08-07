using FleetErp.Domain.Common;
using FleetErp.Domain.Entities.Accounting;
using FleetErp.Domain.Entities.Lookups;

namespace FleetErp.Domain.Entities.Customers;

/// <summary>
/// Represents a customer (driver) who rents vehicles.
/// Each customer has an associated Account for tracking financial transactions.
/// </summary>
public class Customer : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? NationalId { get; set; }
    public string? DrivingLicenseNumber { get; set; }
    public DateTime? DrivingLicenseExpiry { get; set; }
    public string? Address { get; set; }
    public string? Notes { get; set; }

    public int StatusId { get; set; }
    public int? AccountId { get; set; }

    // Navigation properties
    public LookupItem Status { get; set; } = null!;
    public Account? Account { get; set; }
    public ICollection<CustomerDocument> Documents { get; set; } = [];
}
