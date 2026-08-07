using FleetErp.Domain.Common;
using FleetErp.Domain.Entities.Lookups;
using FleetErp.Domain.Entities.Vehicles;

namespace FleetErp.Domain.Entities.Insurance;

/// <summary>
/// Represents an insurance policy for a vehicle.
/// Tracks policy details, coverage, premiums, and expiration for the notification system.
/// </summary>
public class InsuranceRecord : BaseEntity
{
    public int VehicleId { get; set; }
    public int InsuranceCompanyId { get; set; }
    public int InsuranceTypeId { get; set; }
    public int StatusId { get; set; }

    public string PolicyNumber { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Premium { get; set; }
    public decimal? CoverageAmount { get; set; }
    public decimal? Deductible { get; set; }
    public string? CoverageDetails { get; set; }
    public string? Notes { get; set; }

    /// <summary>
    /// Whether a renewal reminder notification has been sent.
    /// Used by the notification system to avoid duplicate reminders.
    /// </summary>
    public bool RenewalReminderSent { get; set; }

    // Navigation properties
    public Vehicle Vehicle { get; set; } = null!;
    public InsuranceCompany InsuranceCompany { get; set; } = null!;
    public LookupItem InsuranceType { get; set; } = null!;
    public LookupItem Status { get; set; } = null!;
    public ICollection<InsuranceDocument> Documents { get; set; } = [];
}
